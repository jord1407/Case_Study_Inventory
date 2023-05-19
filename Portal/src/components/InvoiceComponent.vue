<template>
    <div class="container">
        <h3>Invoice</h3>

        <br />

        <div class="row">
            <div class="col">
                <label><b>Id:</b> {{ invoice.id }}</label>
            </div>
            <div class="col">
                <label><b>Date:</b> {{ format_date(new Date(invoice.date + 'Z')) }}</label>
            </div>
            <div class="col">
                <label><b>Year:</b> {{ invoice.year }}</label>
            </div>
            <div class="col">
                <label><b>Month:</b> {{ invoice.month }}</label>
            </div>
        </div>

        <br />

        <div class="row">
            <div class="col">
                <label><b>Total:</b> {{ invoice.total }}</label>
            </div>
            <div class="col"></div>
            <div class="col"></div>
            <div class="col"></div>
        </div>
        
        <AssetsComponent :assetList="invoice.assets" :invoiceId="invoice.id"></AssetsComponent>

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { useRoute, useRouter } from "vue-router";
    import moment from 'moment';
    import { Invoice } from '../models/Invoice';
    import AssetsComponent from './AssetsComponent.vue'

    export default defineComponent({
        name: 'Invoice-Component',
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
        components: {
            AssetsComponent
        },
        watch: {
            invoice: function (val) {
                AssetsComponent.assetList = val.assets;
            }
        },
        data() {
            return {
                invoice: new Invoice()
            }
        },
        created() {
            if (this.id != '0')
                this.fetchData()
        },
        methods: {
            fetchData() {
                fetch('https://localhost:55006/api/Invoice/' + this.id, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*"
                    },
                })
                    .then((response) => {
                        response.json().then((data: Invoice) => {
                            this.invoice = data;
                        })
                    })
            },
            format_date(value: Date | undefined) {
                if (value) {
                    return moment(String(value)).format('YYYY-MM-DD HH:mm:ss')
                }
            },
        }
    });
</script>