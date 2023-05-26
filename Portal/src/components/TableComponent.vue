<template>
    <table class="table" :key="updateKey">
        <thead>
            <tr v-if="headers">
                <th scope="col" style="text-align: center" v-for="header in headers" v-bind:key="header.id" @click="sort(header)">
                    {{ header.name }}
                    &nbsp;
                    <i class="fa fa-sort" aria-hidden="true" v-if="header.sortDirection == 0"></i>
                    <i class="fa fa-sort-asc" aria-hidden="true" v-if="header.sortDirection == 1"></i>
                    <i class="fa fa-sort-desc" aria-hidden="true" v-if="header.sortDirection == 2"></i>
                </th>
                <th scope="col" style="text-align: center" v-if="displayAction">Actions</th>
            </tr>
        </thead>
        <tbody v-if="list">
            <tr v-for="item in list" v-bind:key="item.id">
                <td style="text-align: center" v-for="header in headers" v-bind:key="header">
                    <span v-if="header.method">{{ header.method(item[header.name.toLowerCase()]) }}</span>
                    <span v-if="!header.method && !header.format">{{ item[header.name.toLowerCase()] }}</span>
                </td>

                <td style="text-align: center" v-if="displayAction">
                    <span v-for="action in actions" v-bind:key="action.id">
                        <span v-if="action.isRoute">
                            <router-link :to="action.route + item.id" style="text-decoration: none;">{{ action.name }}</router-link>
                        </span>
                        <span v-if="actions.length > 1">&nbsp;&nbsp;</span>
                        <span v-if="!action.isRoute">
                            <a href="#" @click="action.method(item.id)" style="text-decoration: none; color: #ff0000">Delete</a>
                        </span>
                    </span>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { useRoute, useRouter } from "vue-router";

    import { IModel } from '../models/IModel'

    export default defineComponent({
        name: 'Table-Component',
        props: {
            list: {
                Type: Array<IModel>(),
                default: null
            },
            headers: {
                Type: Array<{ id: number, name: string, sortDirection: number, method: any }>(),
                default: null
            },
            displayAction: {
                Type: Boolean,
                default: false
            },
            actions: {
                Type: Array<{ id: number, name: string, method: any, isRoute: Boolean, route: string }>(),
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
                updateKey: 0
            }
        },
        methods: {
            sort(header: { id: number, name: string, sortDirection: number, method: any }) {
                if (header.sortDirection == 0) {
                    header.sortDirection = 1; //sorted asc
                }
                else if (header.sortDirection == 1) {
                    header.sortDirection = 2; //sorted desc
                }
                else {
                    header.sortDirection = 0; //unsorted
                }

                // reset the sorting of the others
                this.headers.forEach((x: { id: number, name: string, sortDirection: number, method: any }) => {
                    if (x.id != header.id)
                        x.sortDirection = 0;
                });

                this.updateKey++;

                console.log(this.headers.find(x => x.id == header.id).name + " - " + this.headers.find(x => x.id == header.id).sortDirection);
            }
        }
    });
</script>