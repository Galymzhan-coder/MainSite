<template>
  <quill-editor v-model:value="content"
                :options="editorOption"
                :disabled="disabled"
                class="quill-editor"
                @blur="onEditorBlur"
                @focus="onEditorFocus"
                @ready="onEditorReady"
                @change="onEditorChange" />
</template>

<script setup>
  import { ref, reactive, watch, defineEmits, defineProps } from 'vue';
  //import hljs from 'highlight.js';

  const props = defineProps({
    modelValue: {
      type: String,
      required: true
    },
    disabled: {
      type: Boolean,
      default: false
    }
  });

  // Эмит для обновления значения родительского компонента
  const emit = defineEmits(['update:modelValue']);

  // Локальная ссылка на контент редактора
  const content = ref(props.modelValue);

  // Настройки Quill редактора
  const editorOption = reactive({
    placeholder: 'Введите текст...',
    modules: {
      //syntax: { hljs },
      toolbar: [
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
      ]
    }
  });
 
  // Следим за изменениями пропса modelValue
  watch(() => props.modelValue, (newValue) => {
    if (content.value !== newValue) {
      content.value = newValue;
    }
  });

  // События Quill редактора
  const onEditorBlur = (quill) => {
    console.log("editor blur!", quill);
  };

  const onEditorFocus = (quill) => {
    console.log("editor focus!", quill);
  };

  const onEditorReady = (quill) => {
    console.log("editor ready!", quill);
  };

  const onEditorChange = ({ quill, html }) => {
    emit('update:modelValue', html);
    console.log(quill)
    console.log(html)

  };
</script>

<style scoped>
  .quill-editor {
    min-height: 200px;
  }
</style>
