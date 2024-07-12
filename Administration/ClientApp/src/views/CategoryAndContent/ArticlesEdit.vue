<template>
 
  <div class="p-4 border">
    <h2 class="md:font-bold">Редактировать запись</h2>
    <div class="flex flex-col">
      <div class="grid grid-cols-6 gap-0" style="grid-template-columns: 1350px">

        <form @submit.prevent="saveItem" v-if="itemsEdit">
          <div class="flex flex-row -mx-2">
            <div class="flex flex-col border w-2/3 mx-2">
              <div class="bg-gray-300 p-2 border md:font-bold"><label>Заголовок</label></div>

              <input v-model="itemsEdit.title" type="text" />

              <div class="bg-gray-300 p-2 border md:font-bold"><label>Категория</label></div>
              <Dropdown :items="selectItems"
                        idField="id"
                        displayField="title"
                        v-model="itemsEdit.category_id" />

              <div class="bg-gray-300 p-2 border md:font-bold"><label>Текст</label></div>
 

              <div class="p-2">

                <quill-editor v-model="content"
                              class="quill-editor"
                              :options="state.editorOption"
                              :disabled="state.disabled"
                              @blur="onEditorBlur($event)"
                              @focus="onEditorFocus($event)"
                              @ready="onEditorReady($event)"
                              @change="onEditorChange($event)" />

              </div>

              <div class="bg-gray-300 p-2 border md:font-bold"><label>ЧПУ</label></div>
              <input v-model="itemsEdit.sefname" type="text" />

            </div>

            <div class="flex flex-col w-1/3 mx-2 mx-2 ">
              <div class="p-2 border w-full text-left mx-2 mx-2 inline-flex items-center md:font-bold w-full">
                <label>Активность:</label> &nbsp;
                <input v-model="itemsEdit.is_active" type="checkbox" class="mr-2" />

              </div>
              <div class="flex flex-col border w-1/3 mx-2 mx-2 w-full">
                <label class="md:font-bold">Мета-теги</label> &nbsp;
                <div class="p-2">
                  <label class="md:font-bold">Meta Title</label> &nbsp;
                </div>
                <input v-model="itemsEdit.meta_title" type="text" class="border w-full" />
                <div class="p-2 md:font-bold">
                  <label>Meta Keywords</label> &nbsp;
                </div>
                <input v-model="itemsEdit.meta_keywords" type="text" class="border w-full" />
                <div class="p-2 md:font-bold">
                  <label>Meta Description</label> &nbsp;
                </div>
                <input v-model="itemsEdit.meta_description" type="text" class="border w-full" />
              </div>
            </div>
          </div>
          <div class="p-2 w-full inline-flex items-center space-x-4">
            <button type="submit" class="bg-gray-300 py-2 px-4 rounded">Сохранить</button>
            <button @click="cancel" class="bg-gray-300 py-2 px-4 rounded">Отмена</button>
          </div>
        </form>
      </div>
    </div>
    <!--</div>-->
  </div>
</template>

<script setup>
  import { ref, defineProps, defineEmits, onMounted, watch } from 'vue';
  import { useRouter, useRoute, } from 'vue-router';
  import ApiService from '../../services/api-service.js';
  import { reactive } from 'vue';
  import { quillEditor } from 'vue3-quill';
  import Dropdown from "@/components/DropdownSelector.vue";

  const props = defineProps(['item']);
  const emit = defineEmits(['save', 'cancel']);

  const formData = ref({ ...props.item });
  const route = useRoute();

  let itemsEdit = ref(null);
  const apiService = new ApiService();

  const router = useRouter();
  let content = "";
  let quillInstance = ref(null);

  const selectItems = ref([]);
  /*
  const fetchItems = async () => {
      try {
        const response = await apiService.fetchDataByTypeLang('Index', 'category', 1);
        selectItems.value = response.data;
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    */
  async function saveItem() {

    try {
      let id = route.params.id;

      const postData = { ...itemsEdit.value };
      console.log("saveItem postData=", postData);

      var req = `Update?type=text_pages&id=${id}`;

      if (Number(id) == 0)
      {
        req = `Create?type=text_pages`;
      }

      await apiService.sendData(req, postData, 'post')
        .then(response => {

          console.log("saveItem response=", response);
          // Обработка успешного ответа
        })
        .catch(error => {
          // Обработка ошибки
          console.log("saveItem, sendData error=", error);
        });

    } catch (error) {
      console.log("saveItem, sendData error = ", error);
    }

    emit('save', formData.value);
    goToPage('/Articles');
  }

  function cancel() {
    emit('cancel');
    goToPage('/Articles');
  }
  onMounted(async () => {

    let id = route.params.id;

    //if (Number(id) == 0)
    //  return;
    //fetchItems();
    const editData = await apiService.fetchDataByTypeId('GetItem', 'text_pages', id);
    itemsEdit.value = editData;
    console.log('ArticlesEdit, itemsEdit.value=', itemsEdit.value, ", id=", id);

    selectItems.value = await apiService.fetchDataByTypeLang('Index', 'category', 1);

    console.log('Articles, selectItems.value=', selectItems.value);

    content = itemsEdit.value.text;
    state._content = itemsEdit.value.text;

  });

  function goToPage(url) {
    router.push(url);
  }

  const uploadImage = async (file) => {
    const formData = new FormData();
    formData.append('file', file);

    try {
      const response = await apiService.sendFile('upload-image', formData);
      console.log('uploadImage response = ', response);
      return response;
    } catch (error) {
      console.error('Failed to upload image:', error);
      return null;
    }
  }

  const state = reactive({
    dynamicComponent: null,
    content: '<p>Initial Content</p>',
    _content: '',
    editorOption: {
      placeholder: 'core',
      modules: {
        toolbar: {
          container: [
            ['bold', 'italic', 'underline', 'strike'],
            ['blockquote', 'code-block'],
            [{ header: 1 }, { header: 2 }],
            [{ list: 'ordered' }, { list: 'bullet' }],
            [{ script: 'sub' }, { script: 'super' }],
            [{ indent: '-1' }, { indent: '+1' }],
            [{ direction: 'rtl' }],
            [{ size: ['small', false, 'large', 'huge'] }],
            [{ header: [1, 2, 3, 4, 5, 6, false] }],
            [{ color: [] }, { background: [] }],
            [{ font: [] }],
            [{ align: [] }],
            ['clean'],
            ['link', 'image', 'video']
          ],
          handlers: {
            image: () => {
              const input = document.createElement('input');
              input.setAttribute('type', 'file');
              input.click();

              input.onchange = async () => {
                const file = input.files[0];
                const imageUrl = await uploadImage(file);
                if (imageUrl) {
                  true
                  const range = quillInstance.value.getSelection(true);
                  quillInstance.value.insertEmbed(range.index, 'image', imageUrl);
                }
              };
            }
          }
        }
        // more options
      },
      disabled: false
    }
  });
  const onEditorBlur = quill => {
    console.log('editor blur!', quill)
  }
  const onEditorFocus = quill => {
    console.log('editor focus!', quill)
  }
  const onEditorReady = quill => {
    quillInstance.value = quill;

    //if (itemsEdit.value && itemsEdit.value.description) {
    content = itemsEdit.value.text;
    //}
    let delta = quill.clipboard.convert(content);
    quill.setContents(delta, 'silent');

    console.log('editor ready!', quill, ', content=', content);
  }
  const onEditorChange = ({ quill, html, text }) => {

    console.log('editor change! quill=', quill, ", html=", html, ", text=", text, ", itemsEdit=", itemsEdit, ", this.state=", state);

    state._content = html;
    itemsEdit.value.text = html;


  }

  watch(() => itemsEdit.value, (newVal) => {
    if (newVal && newVal.text) {
      content = newVal.text;
      if (quillInstance.value) {
        quillInstance.value.setContents(quillInstance.value.clipboard.convert(content));
      }
    }
  }, { immediate: true, deep: true });

</script>

<style>
  /* Стили для модального окна (пример) */
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background-color: white;
    padding: 20px;
  }

  .quill-editor {
    min-height: 200px;
    height: auto;
    overflow-y: hidden;
  }
</style>
