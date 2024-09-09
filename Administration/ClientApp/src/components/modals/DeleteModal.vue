<template>
  <div v-if="props.isVisible" class="fixed inset-0 z-10 flex justify-center items-center bg-black bg-opacity-50">
    <div class="bg-white p-5 rounded-lg shadow-lg text-center">
      <h3 class="text-lg font-bold mb-4">Удалить запись?</h3>
      <p class="mb-6">Вы уверены, что хотите удалить эту запись? Это действие необратимо.</p>
      <div class="flex justify-around space-x-4">
        <button @click="confirmDelete" class="bg-red-500 text-white px-4 py-2 rounded-lg hover:bg-red-600">Удалить</button>
        <button @click="cancel" class="bg-gray-500 text-white px-4 py-2 rounded-lg hover:bg-gray-600">Отмена</button>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { defineProps, defineEmits } from 'vue';
  import { toast } from "vue3-toastify";
  import ApiService from '@/services/api-service.js';
  import { useRouter } from 'vue-router';

  const router = useRouter();
  const props = defineProps({
    isVisible: Boolean,
    type: String,
    recordId: Number
  });
  const apiService = new ApiService();
  const emit = defineEmits(['handle-delete-modal']);

  const confirmDelete = async () => {
    emit('handle-delete-modal', false);
    const pendingToastId = toast.loading('Запрос выполняется...');
    try {
      const data = await apiService.sendDataTypeId('Delete', props.type, props.recordId);

      if (data.success) {
          toast.update(pendingToastId, {
            render: 'Запись успешно удалено 👌',
            type: 'success',
            isLoading: false,
            autoClose: 2000
          });
        router.go(0);
      } else {
          // Если есть ошибка с данными, показываем "error" тост
          throw new Error('Ошибка при удалении категории блога');
        }

        return data; // Возвращаем результат запроса
    } catch (error) {
        // Если запрос не удался, обновляем тост с ошибкой
        toast.update(pendingToastId, {
          render: 'Ошибка при удалении записи 🤯',
          type: 'error',
          isLoading: false,
          autoClose: 2000
        });

        // Пробрасываем ошибку дальше, если нужно обработать ее выше
        throw error;
      }
    }

  const cancel = () => {
    emit('handle-delete-modal', false); // вызываем событие подтверждения удаления
  };
</script>

<style scoped>
  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .modal-content {
    background: ;
    padding: 20px;
    border-radius: 5px;
    text-align: center;
  }

  .modal-actions {
    display: flex;
    justify-content: space-around;
    margin-top: 20px;
  }

  .btn-delete {
    background-color: red;
    color: white;
    padding: 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
  }

  .btn-cancel {
    background-color: gray;
    color: white;
    padding: 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
  }
</style>
