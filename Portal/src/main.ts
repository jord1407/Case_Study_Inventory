import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHashHistory } from 'vue-router'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import { VueSignalR } from '@dreamonkey/vue-signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';

import AssetsComponent from './components/AssetsComponent.vue'
import AssetComponent from './components/AssetComponent.vue'
import InvoicesComponent from './components/InvoicesComponent.vue'
import InvoiceComponent from './components/InvoiceComponent.vue'


const routes = [
    { path: '/', component: AssetsComponent },
    { path: '/:id', component: AssetComponent },
    { path: '/invoices', component: InvoicesComponent },
    { path: '/invoices/:id', component: InvoiceComponent },
]

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHashHistory(),
    routes, // short for `routes: routes`
})

// Create your connection
// See https://docs.microsoft.com/en-us/javascript/api/@microsoft/signalr/hubconnectionbuilder
const connection = new HubConnectionBuilder()
    .withUrl('https://localhost:55010/generatorhub', {
        headers: {
            "Access-Control-Allow-Origin": "*"
        },
        withCredentials: false
    })
    .build();

const app = createApp(App)

app.use(router)
app.use(VueSignalR, { connection })
app.mount('#app')
