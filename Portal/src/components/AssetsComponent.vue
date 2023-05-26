<template>
    <div class="container">
        <h3 v-if="!invoiceId">
            Assets
            &nbsp;
            &nbsp;
            &nbsp;
            <span><router-link to="/0" class="btn btn-primary">New</router-link></span>
        </h3>

        <br />

        <TableComponent :list="assets" :headers="headers" :displayAction="!invoiceId" :actions="actions"></TableComponent>

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { Asset } from '../models/Asset'
    import { useRoute, useRouter } from "vue-router";
    import moment from 'moment';

    import TableComponent from './TableComponent.vue'

    export default defineComponent({
        name: 'Assets-Component',
        props: {
            assetList: {
                Type: Array<Asset>(),
                default: null
            },
            invoiceId: {
                Type: Number,
                default: null
            }
        },
        components: {
            TableComponent
        },
        watch: {
            assets: function (val) {
                TableComponent.list = val;
            },
            headers: function (val) {
                TableComponent.headers = val
            },
            actions: function (val) {
                TableComponent.actions = val
            }
        },
        setup() {
            const route = useRoute();
            const router = useRouter();
            const id = route.params.id;
            const headers: string[] = [
                "Id",
                "Name",
                "Price",
                "ValidFrom",
                "ValidTo"
            ];

            return {
                id,
                route,
                router,
                headers
            }
        },
        data() {
            const actions: { id: number, name: string, method: any, isRoute: Boolean, route: string }[] = [
                { id: 1, name: "Edit", method: null, isRoute: true, route: "/" },
                { id: 2, name: "Delete", method: (id: number) => this.deleteRow(id), isRoute: false, route: null },
            ]

            return {
                assets: Array<Asset>(),
                actions
            }
        },
        created() {
            this.fetchData()
        },
        updated() {
            if (this.invoiceId != null)
                this.assets = this.assetList
        },
        methods: {
            fetchData() {
                fetch(process.env.VUE_APP_ASSETAPI + '/api/Asset', {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*"
                    },
                })
                    .then((response) => {
                        response.json().then((data: Asset[]) => {
                            this.assets = data;
                        })
                    })
            },
            deleteRow(id: number) {
                fetch(process.env.VUE_APP_ASSETAPI + '/api/Asset/' + id, {
                    method: "DELETE",
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*"
                    },
                })
                    .then(() => {
                        this.fetchData();
                    })
            },
            format_date(value: Date) {
                if (value) {
                    return moment(String(value)).format('YYYY-MM-DD')
                }
            },
        }
    });
</script>