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

            return {
                id,
                route,
                router
            }
        },
        data() {
            const headers: { id: number, name: string, sortDirection: number, method: any }[] = [
                { id: 1, name: "Id", sortDirection: 0, method: null },
                { id: 2, name: "Name", sortDirection: 0, method: null },
                { id: 3, name: "Price", sortDirection: 0, method: (price: number) => price.toLocaleString("en-US", { style: "currency", currency: "CAD" }) },
                { id: 4, name: "ValidFrom", sortDirection: 0, method: (value: Date) => this.format_date(value) },
                { id: 5, name: "ValidTo", sortDirection: 0, method: (value: Date) => this.format_date(value) }
            ];

            const actions: { id: number, name: string, method: any, isRoute: Boolean, route: string }[] = [
                { id: 1, name: "Edit", method: null, isRoute: true, route: "/" },
                { id: 2, name: "Delete", method: (id: number) => this.deleteRow(id), isRoute: false, route: null },
            ]

            return {
                assets: Array<Asset>(),
                actions,
                headers
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