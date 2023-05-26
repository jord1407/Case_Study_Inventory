<template>
    <table class="table">
        <thead>
            <tr v-if="headers">
                <th scope="col" style="text-align: center" v-for="header in headers" v-bind:key="header">{{ header }}</th>
                <th scope="col" style="text-align: center" v-if="displayAction">Actions</th>
            </tr>
        </thead>
        <tbody v-if="list">
            <tr v-for="item in list" v-bind:key="item.id">
                <td style="text-align: center" v-for="header in headers" v-bind:key="header">{{ item[header.toLowerCase()] }}</td>
                
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
    import moment from 'moment';

    import { IModel } from '../models/IModel'

    export default defineComponent({
        name: 'Table-Component',
        props: {
            list: {
                Type: Array<IModel>(),
                default: null
            },
            headers: {
                Type: Array<string>(),
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
        methods: {
            format_date(value: Date) {
                if (value) {
                    return moment(String(value)).format('YYYY-MM-DD')
                }
            },
        }
    });
</script>