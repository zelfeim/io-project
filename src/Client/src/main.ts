import { createApp, type Plugin } from 'vue';
import App from '@/App.vue';
import router from '@/router';
import elementPlus from 'element-plus';
import './styles/main.scss';
import { createPinia } from 'pinia';
import piniaPluginPersistedState from 'pinia-plugin-persistedstate';
import { VueQueryPlugin } from '@tanstack/vue-query';

const app = createApp(App);

const pinia: Plugin = createPinia().use(piniaPluginPersistedState);

app.use(router).use(elementPlus).use(pinia).use(VueQueryPlugin).mount('#app');
