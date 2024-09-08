<template>
  <div class="p-4 border">
    <div>
      <h2>Редактировать запись</h2>

      <div class="flex flex-col">
        <div class="grid grid-cols-6 gap-0" style="grid-template-columns: 550px">
          <form @submit.prevent="saveItem" v-if="itemsEdit">

            <div class="bg-gray-300 p-2 border"><label>Наименование:</label></div>
            <input type="text" class="p-2 border w-full text-left" v-model="itemsEdit.title" />

            <div class="bg-gray-300 p-2 border"><label>ЧПУ:</label></div>
            <input v-model="itemsEdit.sefname" type="text" class="p-2 border w-full text-left" />

            <div class="bg-gray-300 p-2 border"><label>Родитель:</label></div>
            <div class="p-2 border w-full text-left">
              <select v-model="selectedItem">
                <option disabled value="">Выберите значение</option>
                <option v-for="item in items" :key="item.id" :value="item">{{ item.title }}</option>
              </select>
            </div>

            <div class="bg-gray-300 p-2 border"><label>Связанный продукт:</label></div>
            <input type="text" class="p-2 border w-full text-left" v-model="itemsEdit.product_id" />

 
            <div class="p-2 border w-full text-left inline-flex items-center">
              <label>Активность:</label> &nbsp;
              <input v-model="itemsEdit.is_active" type="checkbox" class="mr-2" />

            </div>
            <div class="p-2"></div>
            <div class="p-2 w-full inline-flex items-center space-x-4">
              <button type="submit" class="bg-gray-300 py-2 px-4 rounded">Сохранить</button>
              <button @click="cancel" class="bg-gray-300 py-2 px-4 rounded">Отмена</button>
            </div>


          </form>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
  import { ref, defineProps, defineEmits, onMounted/*, computed*/ } from 'vue';

  //import RichTextEditor from '../../components/RichTextEditor';
  //import TextEditor from '../../components/TextEditor';
  import { useRouter, useRoute, } from 'vue-router';
  import ApiService from '../../services/api-service.js';


  const items = ref([]);
  const apiService = new ApiService();
  let itemsEdit = ref(null);
  let selectedItem = ref(null);
 // let selectedItemProduct = ref(null);
  //let content = "";
  //let quillInstance = ref(null);
  /*
  onMounted(async () => {
      const { data } = apiService.fetchData('CategoryIerarchyList')
      .then(data => {
        console.log('Fetched data:', data);
      })
      .catch(error => {
        console.error('Error:', error);
      });

      this.items = data;
  });
  */

  const router = useRouter();
  const route = useRoute();

  onMounted(async () => {
    try {
      const data = await apiService.fetchDataByType('Index', 'faq_category');
      items.value = data;
      //console.log("router=", router, ", id=", route.params.id);
      //selectedItem = ref(items.value.find(item => item.id === itemsEdit.parentId)?.title || null);
      //selectedItem = ref(items.value.map(item => item.id == itemsEdit.parentId).values);

      let id = route.params.id;
      //const editData = await apiService.fetchDataById('GetCategoryItem', id);
      const editData = await apiService.fetchDataByTypeId('GetItem', 'faq_category', id);
      itemsEdit.value = editData;
      selectedItem = items.value.find(item => item.id === editData.parent_id);

      //}
      //formItems.id = editData.id;

      //itemsEdit.value.is_active = itemsEdit.value.is_active === 1 ? true : false;

      console.log("FaqCategoryEdit id=", id, ", route.params=", route.params, ", data=", data, ", selectedItem=", selectedItem, ", editData=", editData);
      console.log("itemsEdit=", itemsEdit);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  });
  /*
  watch(() => itemsEdit.value, (newVal) => {
    if (newVal && newVal.description) {
      content = newVal.description;
      if (quillInstance.value) {
        quillInstance.value.setContents(quillInstance.value.clipboard.convert(content));
      }
    }
  }, { immediate: true, deep: true });
  */

  const props = defineProps(['item']);
  const emit = defineEmits(['save', 'cancel']);

  const formData = ref({ ...props.item });

  async function saveItem() {


    console.log('this.itemsEdit =', itemsEdit);

    try {
      let id = route.params.id;
      //const postData = { type: 'categories', id: id, dto: itemsEdit };
      console.log("saveItem itemsEdit.value=", itemsEdit.value);
      //const postData = { dto: { ...itemsEdit.value } };
      const postData = { ...itemsEdit.value };
      console.log("saveItem postData=", postData);
      //await apiService.sendData('Update', postData);

      //await apiService.sendData(`Update/categories/${id}`, postData, 'post')
      await apiService.sendData(`Update?type=faq_category&id=${id}`, postData, 'post')
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
    goToPage('/FaqCategories');
  }

  function cancel() {
    //emit('cancel');
    goToPage('/FaqCategories');
  }

  function goToPage(url) {
    router.push(url);
  }
  /*
  onMounted(async () => {
    try {
      const data = await apiService.fetchData('Index');
      items.value = data;
      var id = this.$route.params.id;
      console.log('id=',id);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  });

  onMounted(async () => {
    try {
      const data = await apiService.fetchData('GetCategoryItem');
      items.value = data;
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  });
  */



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
</style>
