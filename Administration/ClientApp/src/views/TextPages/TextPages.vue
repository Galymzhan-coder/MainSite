<template>
  <div class="p-4 border">
    <button @click="goToPageById('TextPagesEdit',0)" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mb-4">
      Добавить
    </button>

    <div class="flex flex-col">
      <div class="grid grid-cols-6 gap-0" style="grid-template-columns: 0.5fr 6fr 2fr 2fr 2fr">
        <div class="bg-gray-300 p-2 border">#</div>
        <div class="bg-gray-300 p-2 border">Заголовок</div>
        <div class="bg-gray-300 p-2 border">ЧПУ</div>
        <div class="bg-gray-300 p-2 border">Активность</div>
        <div class="bg-gray-300 p-2 border">Удалить / Редактировать</div>
      </div>

      <div v-for="(item, index) in items" :key="item.id" class="grid grid-cols-6 gap-0" style="grid-template-columns: 0.5fr 6fr 2fr 2fr 2fr">
        <div class="p-2 border">{{ index + 1 }}</div>
        <div class="p-2 border">{{ item.title }}</div>
        <div class="p-2 border">{{ item.sefname }}</div>
        <div class="p-2 border">
          <input type="checkbox" v-model="item.is_active" @change="() => ChangeData(item)" />
        </div>

        <div class="p-2 border">
          <button @click="deleteItem(item.id)" class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 mr-2">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="4" y1="7" x2="20" y2="7" />  <line x1="10" y1="11" x2="10" y2="17" />  <line x1="14" y1="11" x2="14" y2="17" />  <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />  <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
          </button>
          <button @click="goToPageById('TextPagesEdit',item.id)" class="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4" />  <line x1="13.5" y1="6.5" x2="17.5" y2="10.5" /></svg>
          </button>
        </div>
      </div>
    </div>
    <textPagesEdit v-if="showForm"
                   :item="selectedItem"
                   @save="saveItem"
                   @cancel="cancelForm" />


  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import './TextPagesEdit.vue';

  //import { useRouter } from 'vue-router';
  import ApiService from '../../services/api-service.js';

  import { useRouter } from 'vue-router';

  const router = useRouter();
  const apiService = new ApiService();

  const items = ref([]);

  onMounted(async () => {
    try {
      //const data = await apiService.fetchData('Index');
      const data = await apiService.fetchDataByTypeLang('Index', 'contents', 1);

      items.value = data;//data.sort((a,b) => a.id - b.id);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  });
  

  const selectedItem = ref(null);
  const showForm = ref(false);
  
  function saveItem(updatedItem) {
    // Сохранение обновленных данных в items
    const index = items.value.findIndex((item) => item.id === updatedItem.id);
    if (index !== -1) {
      items.value.splice(index, 1, updatedItem);
    }
    // Закрываем форму после сохранения
    showForm.value = false;
  }

  function cancelForm() {
    // Закрываем форму при отмене
    showForm.value = false;
  }

  function goToPageById(url, id) {
    router.push({ name: url, params: { id: id } });
  }


  async function ChangeData(item) {
    try {
      console.log("ChangeData, item = ", item);
      await apiService.sendData(`Update?type=content&id=${item.id}`, item, 'post')
    } catch (e) {
      console.log("ChangeData, sendData error = ", e);
    }

  }

  async function deleteItem(id) {

    try {

      var req = `Delete?type=contents&id=${id}`;

      await apiService.sendData(req, null, 'post')
        .then(response => {
          console.log("saveItem response=", response);
        })
        .catch(error => {
          console.log("saveItem, sendData error=", error);
        });

      items.value = items.value.filter(item => item.id !== id);

    } catch (error) {
      console.log("saveItem, sendData error = ", error);
    }
  }

</script>
