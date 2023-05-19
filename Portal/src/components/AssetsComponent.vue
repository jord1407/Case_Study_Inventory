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

        <table class="table">
            <thead>
                <tr>
                    <th scope="col" style="text-align: center">Id</th>
                    <th scope="col" style="text-align: center">Name</th>
                    <th scope="col" style="text-align: center">Price</th>
                    <th scope="col" style="text-align: center">ValidFrom</th>
                    <th scope="col" style="text-align: center">ValidTo</th>
                    <th scope="col" style="text-align: center" v-if="!invoiceId">Actions</th>
                </tr>
            </thead>
            <tbody v-if="assets">
                <tr v-for="asset in assets" v-bind:key="asset.id">
                    <th scope="row" style="text-align: center">{{ asset.id }}</th>
                    <td style="text-align: left">{{ asset.name }}</td>
                    <td style="text-align: left">{{ asset.price.toLocaleString("en-US", { style: "currency", currency: "CAD" }) }}</td>
                    <td style="text-align: center">{{ format_date(asset.validFrom) }}</td>
                    <td style="text-align: center">{{ format_date(asset.validTo) }}</td>
                    <td style="text-align: center" v-if="!invoiceId">
                        <span>
                            <router-link :to="'/' + asset.id" style="text-decoration: none;">Edit</router-link>
                        </span>
                        &nbsp;&nbsp;
                        <span>
                            <a href="#" @click="deleteRow(asset.id)" style="text-decoration: none; color: #ff0000">Delete</a>
                        </span>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { Asset } from '../models/Asset'
    import { useRoute, useRouter } from "vue-router";
    import moment from 'moment';

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
            return {
                assets: Array<Asset>()
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
                fetch('https://localhost:55008/api/Asset', {
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
                fetch('https://localhost:55008/api/Asset/' + id, {
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