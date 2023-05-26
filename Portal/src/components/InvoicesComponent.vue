<template>
    <div class="container">
        <div class="container">
            <h3>
                Invoices
                &nbsp;
                &nbsp;
                &nbsp;
                <span><button ref="btnGenerate" @click="generate" class="btn btn-primary">{{ buttonText }}</button></span>
            </h3>

            <br />

            <TableComponent :list="invoices" :headers="headers" :displayAction="true" :actions="actions"></TableComponent>

        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { Invoice } from '../models/Invoice';
    import { useRoute, useRouter } from "vue-router";
    import { useSignalR } from '@dreamonkey/vue-signalr';
    import moment from 'moment';

    import TableComponent from './TableComponent.vue'

    export default defineComponent({
        name: 'Invoices-Component',
        setup() {
            const route = useRoute();
            const router = useRouter();
            const signalr = useSignalR();
            const id = route.params.id;

            return {
                id,
                route,
                router,
                signalr
            }
        },
        components: {
            TableComponent
        },
        watch: {
            invoices: function (val) {
                TableComponent.list = val;
            },
            headers: function (val) {
                TableComponent.headers = val
            },
            actions: function (val) {
                TableComponent.actions = val
            }
        },
        data() {
            const headers: { id: number, name: string, sortDirection: number, method: any }[] = [
                { id: 1, name: "Id", sortDirection: 0, method: null },
                { id: 2, name: "Date", sortDirection: 0, method: (value: Date) => this.format_date(new Date(value + 'Z')) },
                { id: 3, name: "Month", sortDirection: 0, method: null },
                { id: 4, name: "Year", sortDirection: 0, method: null },
                { id: 5, name: "Total", sortDirection: 0, method: (price: number) => price.toLocaleString("en-US", { style: "currency", currency: "CAD" }) }
            ];

            const actions: { id: number, name: string, method: any, isRoute: Boolean, route: string }[] = [
                { id: 1, name: "Details", method: null, isRoute: true, route: "/invoices/" },
            ]

            return {
                invoices: Array<Invoice>(),
                buttonText: "Generate",
                message: "",
                actions,
                headers
            }
        },
        created() {
            this.fetchData();

            this.signalr.on('Notification', () => {
                this.notification();
            })
        },
        methods: {
            fetchData() {
                fetch(process.env.VUE_APP_INVOICEAPI + '/api/Invoice', {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*"
                    },
                })
                    .then((response) => {
                        response.json().then((data: Invoice[]) => {
                            this.invoices = data;
                        })
                    })
            },
            generate() {
                fetch(process.env.VUE_APP_INVOICEGENERATORAPI + '/api/InvoiceGenerator/GenerateServiceInvoice', {
                    method: "POST",
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*",
                        "Content-Type": "application/json"
                    },
                    body: "\"" + new Date().toISOString() + "\""
                })
                    .then(() => {
                        this.buttonText = "Generating..."
                        this.$refs.btnGenerate.disabled = true;
                    })
            },
            format_date(value: Date) {
                if (value) {
                    return moment(String(value)).format('YYYY-MM-DD HH:mm:ss')
                }
            },
            notification() {
                this.message = "Generation Successful";

                this.fetchData();
                this.buttonText = "Generate"
                this.$refs.btnGenerate.disabled = false;
            }
        }
    });
</script>