<template>
  <div class="p-4 border">
    <div>
      <h2>Создать новую запись</h2>

      <div class="flex flex-col">
        <div class="grid grid-cols-6 gap-0" style="grid-template-columns: 550px">
          <form @submit.prevent="createItem">

            <div class="bg-gray-300 p-2 border"><label>Наименование:</label></div>
            <input type="text" class="p-2 border w-full text-left" v-model="newItem.title" />

            <div class="bg-gray-300 p-2 border"><label>ЧПУ:</label></div>
            <input v-model="newItem.sefname" type="text" class="p-2 border w-full text-left" />

            <div class="bg-gray-300 p-2 border"><label>Родитель:</label></div>
            <div class="p-2 border w-full text-left">
              <select v-model="selectedItem">
                <option disabled value="">Выберите значение</option>
                <option v-for="item in items" :key="item.id" :value="item">{{ item.title }}</option>
              </select>
            </div>

            <div class="bg-gray-300 p-2 border"><label>Связанный продукт:</label></div>
            <input type="text" class="p-2 border w-full text-left" v-model="newItem.product_id" />

            <div class="p-2 border w-full text-left inline-flex items-center">
              <label>Активность:</label> &nbsp;
              <input v-model="newItem.is_active" type="checkbox" class="mr-2" />
            </div>
            <div class="p-2"></div>
            <div class="p-2 w-full inline-flex items-center space-x-4">
              <button type="submit" class="bg-gray-300 py-2 px-4 rounded">Создать</button>
              <button @click="cancel" class="bg-gray-300 py-2 px-4 rounded">Отмена</button>
            </div>

          </form>
        </div>
      </div>

    </div>
  </div>
</template>

<script setup>
  import { ref, defineEmits, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  import ApiService from '../../services/api-service.js';

  const items = ref([]);
  const apiService = new ApiService();
  let newItem = ref({
    title: '',
    sefname: '',
    product_id: '',
    is_active: false,
    parent_id: null
  });
  let selectedItem = ref(null);

  const router = useRouter();

  onMounted(async () => {
    try {
      const data = await apiService.fetchDataByType('Index', 'faq_category');
      items.value = data;
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  });

  const emit = defineEmits(['save', 'cancel']);

  async function createItem() {
    try {
      // Устанавливаем parent_id выбранного элемента
      newItem.value.parent_id = selectedItem.value ? selectedItem.value.id : null;

      const postData = { ...newItem.value };
      await apiService.sendData('Create?type=faq_category', postData, 'post')
        .then(response => {
          console.log("Item created successfully:", response);
        })
        .catch(error => {
          console.error("Error creating item:", error);
        });

    } catch (error) {
      console.error('Error:', error);
    }

    emit('save', newItem.value);
    goToPage('/FaqCategories');
  }

  function cancel() {
    goToPage('/FaqCategories');
  }

  function goToPage(url) {
    router.push(url);
  }
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
