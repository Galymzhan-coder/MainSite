import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { Form, Field, ErrorMessage, configure } from "vee-validate";
import store from "@/store";
import '@/styles/app.css';
import DatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
import { quillEditor } from 'vue3-quill';
import { Cropper } from 'vue-advanced-cropper';
import 'vue-advanced-cropper/dist/style.css';

configure({
  validateOnInput: true,
});

const app = createApp(App);
app
  .component("Form", Form)
  .component("Field", Field)
  .component("ErrorMessage", ErrorMessage)
  .component("DatePicker", DatePicker)
  .component("Cropper", Cropper);

app.use(router, store);
app.use(quillEditor);

app.mount("#app");
