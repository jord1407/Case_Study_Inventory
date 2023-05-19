<template>
    <div class="container">
        <h3>Asset</h3>

        <br />

        <form v-on:submit="submit">
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" class="form-control" id="name" placeholder="Name" v-model="asset.name">
            </div>

            <br />

            <div class="form-group">
                <div class="col-auto">
                    <label class="sr-only" for="price">Price</label>
                    <div class="input-group mb-2">
                        <div class="input-group-prepend">
                            <div class="input-group-text" style="border-top-right-radius: 0; border-bottom-right-radius: 0; background-color: #e5e5e5;">CA$</div>
                        </div>
                        <input type="number" min="1" step="any" class="form-control" id="price" placeholder="Price" v-model="asset.price">
                    </div>
                </div>
            </div>

            <br />

            <div class="form-group">
                <label for="validFrom">Valid From</label>
                <input type="date" class="form-control" id="validFrom" placeholder="" v-model="validFrom">
            </div>

            <br />

            <div class="form-group">
                <label for="validTo">Valid To</label>
                <input type="date" class="form-control" id="validTo" placeholder="" v-model="validTo">
            </div>

            <br />

            <button type="submit" class="btn btn-primary">Submit</button>
        </form>

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { Asset } from '../models/Asset';
    import { useRoute, useRouter } from "vue-router";
    import moment from 'moment';

    export default defineComponent({
        name: 'Asset-Component',
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
                asset: new Asset()
            }
        },
        created() {
            if (this.id != '0')
                this.fetchData()
        },
        methods: {
            fetchData() {
                fetch('https://localhost:55008/api/Asset/' + this.id, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*"
                    },
                })
                    .then((response) => {
                        response.json().then((data: Asset) => {
                            this.asset = data;
                        })
                    })
            },
            submit(e: any) {
                e.preventDefault();

                let method = "POST";
                let uri = 'https://localhost:55008/api/Asset';

                if (this.id != '0') {
                    method = "PUT";
                    uri = uri + "/" + this.id;
                }

                fetch(uri, {
                    method: method,
                    headers: {
                        "Accept": "application/json",
                        "Access-Control-Allow-Origin": "*",
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(this.asset)
                })
                    .then(() => {
                        this.router.replace("/");
                    })
            },
            format_date(value: Date | undefined) {
                if (value) {
                    return moment(String(value)).format('YYYY-MM-DD')
                }
            },
        },
        computed: {
            validFrom: {
                get() {
                    return this.format_date(this.asset.validFrom);
                },
                set(value) {
                    this.asset.validFrom = value != "" ? value : null;
                }
            },
            validTo: {
                get() {
                    return this.format_date(this.asset.validTo);
                },
                set(value) {
                    this.asset.validTo = value != "" ? value : null;
                }
            }
        },
    });
</script>