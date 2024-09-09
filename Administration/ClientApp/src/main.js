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
import QuillEditorVue from "./components/QuillEditor.vue";
import Vue3Toasity from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import DeleteModal from "@/components/modals/DeleteModal";

configure({
  validateOnInput: true,
});



const app = createApp(App);
app
  .component("Form", Form)
  .component("Field", Field)
  .component("ErrorMessage", ErrorMessage)
  .component("DatePicker", DatePicker)
  .component("Cropper", Cropper)
  .component("quill-editor-vue", QuillEditorVue)
  .component("delete-modal", DeleteModal)


app.use(router, store);
app.use(quillEditor);
app.use(Vue3Toasity, {
  autoClose: 2000,
  limit: 2
});

app.mount("#app");
