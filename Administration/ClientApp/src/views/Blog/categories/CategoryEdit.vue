<template>
  <div class="text-xl font-medium pb-5 flex items-center space-x-2">
    <svg class="h-5 w-5" fill="#000000" height="800px" width="800px" version="1.1" id="Capa_1" viewBox="0 0 458.18 458.18" xml:space="preserve">
    <g>
    <path d="M36.09,5.948c-18.803,0-34.052,15.248-34.052,34.051c0,18.803,15.249,34.052,34.052,34.052   c18.803,0,34.052-15.25,34.052-34.052C70.142,21.196,54.893,5.948,36.09,5.948z" />
    <path d="M147.537,80h268.604c22.092,0,40-17.908,40-40s-17.908-40-40-40H147.537c-22.092,0-40,17.908-40,40S125.445,80,147.537,80z   " />
    <path d="M36.09,132.008c-18.803,0-34.052,15.248-34.052,34.051s15.249,34.052,34.052,34.052c18.803,0,34.052-15.249,34.052-34.052   S54.893,132.008,36.09,132.008z" />
    <path d="M416.142,126.06H147.537c-22.092,0-40,17.908-40,40s17.908,40,40,40h268.604c22.092,0,40-17.908,40-40   S438.233,126.06,416.142,126.06z" />
    <path d="M36.09,258.068c-18.803,0-34.052,15.248-34.052,34.051c0,18.803,15.249,34.052,34.052,34.052   c18.803,0,34.052-15.249,34.052-34.052C70.142,273.316,54.893,258.068,36.09,258.068z" />
    <path d="M416.142,252.119H147.537c-22.092,0-40,17.908-40,40s17.908,40,40,40h268.604c22.092,0,40-17.908,40-40   S438.233,252.119,416.142,252.119z" />
    <path d="M36.09,384.128c-18.803,0-34.052,15.248-34.052,34.051s15.249,34.053,34.052,34.053c18.803,0,34.052-15.25,34.052-34.053   S54.893,384.128,36.09,384.128z" />
    <path d="M416.142,378.18H147.537c-22.092,0-40,17.908-40,40s17.908,40,40,40h268.604c22.092,0,40-17.908,40-40   S438.233,378.18,416.142,378.18z" />
            </g>
        </svg>
    <span>Создание категории:</span>
  </div>
  <div class="p-4 border font-normal text mb-4">
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">Наименование</label>
      <input type="text" v-model="model.title" class="border h-9 pl-3 rounded text-sm" />
    </div>
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">ЧПУ</label>
      <input type="text" v-model="model.sefname" class="border h-9 pl-3 rounded text-sm" />
    </div>
    <div class="flex items-center">
      <input id="active-checkbox" type="checkbox" v-model="model.is_active" class="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
      <label for="active-checkbox" class="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300">Активность</label>
    </div>
  </div>
  <div class="p-4 border font-medium text mb-4">
    <button type="submit" class="border rounded py-1 px-3 bg-gray-500 hover:bg-gray-700 text-white" @click="updateModel(model)">Сохранить</button>
  </div>
</template>

<script setup>
  import { reactive, onMounted, defineProps } from 'vue';
  import ApiService from '@/services/api-service.js';
  import { useRouter } from 'vue-router';
  import { toast } from 'vue3-toastify';
  const router = useRouter();

  const props = defineProps({
    id: {
      type: Number,
      required: false
    }
  })

  const model = reactive({
    title: null,
    sefname: null,
    is_active: false,
  });
  const apiService = new ApiService();


  const showModel = async (id) => {
    const data = await apiService.fetchDataByTypeId('getItem', 'blog_ctg', id);
    if (!data) return;

    Object.assign(model, {
      title: data.title,
      sefname: data.sefname,
      is_active: data.is_active
    });
  };

  const updateModel = async (model) => {
    const pendingToastId = toast.loading('Запрос выполняется...');

    try {
      const { id } = props;
      const data = await apiService.sendDataType('Update', 'blog_ctg', model, 1, id);

      if (data.success) {
        toast.update(pendingToastId, {
          render: 'Запись успешно обновлено 👌',
          type: 'success',
          isLoading: false,
          autoClose: 2000
        });
        router.push({ name: 'blog-category-index' });
      } else {
        // Если есть ошибка с данными, показываем "error" тост
        throw new Error('Ошибка при обновлении категории блога');
      }

      return data; // Возвращаем результат запроса
    } catch (error) {
      // Если запрос не удался, обновляем тост с ошибкой
      toast.update(pendingToastId, {
        render: 'Ошибка при обновдении модели 🤯',
        type: 'error',
        isLoading: false,
        autoClose: 2000
      });

      // Пробрасываем ошибку дальше, если нужно обработать ее выше
      throw error;
    }
  }

  onMounted(() => {
    const { id } = props;
    showModel(id);
  })
</script>

<style>
</style>
