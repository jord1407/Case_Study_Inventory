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

            <table class="table">
                <thead>
                    <tr>
                        <th scope="col" style="text-align: center">Id</th>
                        <th scope="col" style="text-align: center">Date</th>
                        <th scope="col" style="text-align: center">Year</th>
                        <th scope="col" style="text-align: center">Month</th>
                        <th scope="col" style="text-align: center">Total</th>
                        <th scope="col" style="text-align: center">Actions</th>
                    </tr>
                </thead>
                <tbody v-if="invoices">
                    <tr v-for="invoice in invoices" v-bind:key="invoice.id">
                        <th scope="row" style="text-align: center">{{ invoice.id }}</th>
                        <td style="text-align: center">{{ format_date(new Date(invoice.date + 'Z')) }}</td>
                        <td style="text-align: center">{{ invoice.year }}</td>
                        <td style="text-align: center">{{ ("00" + invoice.month).slice(-2) }}</td>
                        <td style="text-align: left">{{ invoice.total.toLocaleString("en-US", { style: "currency", currency: "CAD" }) }}</td>
                        <td style="text-align: center">
                            <span>
                                <router-link :to="'/invoices/' + invoice.id" style="text-decoration: none;">Details</router-link>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { Invoice } from '../models/Invoice';
    import { useRoute, useRouter } from "vue-router";
    import { useSignalR } from '@dreamonkey/vue-signalr';
    import moment from 'moment';

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
        data() {
            return {
                invoices: Array<Invoice>(),
                buttonText: "Generate",
                message: ""
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