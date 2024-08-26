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
    <span>Изменение записи:</span>
  </div>
  <div class="p-4 border font-medium text mb-4">
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">Категория</label>
      <select id="blogs-category_id" class="border h-9 pl-3 rounded text-sm">
        <option>Novosti</option>
        <option>ANketa</option>
        <option>Forum</option>
        <option>Igra</option>
      </select>
    </div>
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">Заголовок</label>
      <input type="text" class="border h-9 pl-3 rounded text-sm" />
    </div>
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">ЧПУ</label>
      <input type="text" class="border h-9 pl-3 rounded text-sm" />
    </div>
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">Дата публикации</label>
      <DatePicker class="custom-datepicker" v-model="date" :format="format" auto-apply partial-flow :flow="['calendar', 'time']" />
    </div>
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">Описание</label>
      <textarea class="border pt-1 pl-3 h-auto rounded text-sm"></textarea>
    </div>
    <div class="grid grid-cols-1 mb-4">
      <label class="text-sm">Текст</label>
      <quill-editor v-model:value="state.content"
                    :options="state.editorOption"
                    :disabled="state.disabled"
                    class="quill-editor"
                    @blur="onEditorBlur($event)"
                    @focus="onEditorFocus($event)"
                    @ready="onEditorReady($event)"
                    @change="onEditorChange($event)" />
    </div>
    <div class="grid grid-cols-2 mb-4 w-[calc(100%)]">
      <div class="grid grid-cols-1">
        <label class="text-sm">Изображение</label>
        <div class="text-sm border text-center p-2 font-medium rounded-md bg-gray-200 border-gray-200 hover:bg-gray-400 transition duration-400
               ease-in-out cursor-pointer w-3/6"
             @click="triggerUploadFile">
          <span>Выберите файл</span>
          <input ref="fileInput" style="display: none" type="file" accept="image/*" @change="uploadFile" />
        </div>
      </div>
      <div class="grid grid-cols-1 w-1/2" v-show="imgObj.imgName && imgObj.imgName.length > 0">
        <label class="text-sm">Названия файла</label>
        <span>{{ imgObj.imgName }}</span>
      </div> 
    </div>
    <div class="" v-show="imgObj.imgName && imgObj.imgName.length > 0">
      <cropper class="cropper-container"
               :src="imgObj.imgSrc"
               :stencil-props="{ aspectRatio: 14/10, movable:true, resizable: true, minWidth: 200, minHeight: 150 }"
               :stecil-size="{ width: 300, height:300 }"
              :resize-image="{
                  adjustStencil: false,
                  minWidth: 100, 
                  minHeight: 100 
                }"
               image-restriction="stencil"
      :style="{
    maxWidth: '100%', 
    maxHeight: '100%', 
    width: imgObj.imgWidth > parentWidth ? '100%' : imgObj.imgWidth + 'px',
    height: 'auto',
    objectFit: 'contain',
    backgroundColor: 'transparent'
  }"
      />
    </div>
    <div class="flex items-center mb-4">
      <input id="useful-checkbox" type="checkbox" value="" class="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
      <label for="useful-checkbox" class="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300">Полезное</label>
    </div>
    <div class="flex items-center mb-4">
      <input id="mainpage-checkbox" type="checkbox" value="" class="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
      <label for="mainpage-checkbox" class="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300">Вывести на главную под слайдером</label>
    </div> 
    <div class="flex items-center">
      <input id="active-checkbox" type="checkbox" value="" class="w-5 h-5 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600">
      <label for="active-checkbox" class="ms-2 text-sm font-medium text-gray-900 dark:text-gray-300">Активность</label>
    </div>
  </div>
  <div class="p-4 border font-medium text mb-4">
    <button type="submit" class="border rounded py-1 px-3 bg-gray-500 hover:bg-gray-700 text-white">Сохранить</button>
   </div>
</template>

<script setup>
import { ref, reactive } from 'vue';

  const date = ref();

  const format = (date) => {
    const year = date.getFullYear();
    const day = `${date.getDate().toString().length === 1 ? '0' + date.getDate() : date.getDate()}`;
    const month = `${date.getMonth().toString().length === 1 ? '0' + date.getMonth() : date.getMonth()}`;
    const hourse = `${date.getHours().toString().length === 1 ? '0' + date.getHours() : date.getHours()}`;
    const minute = `${date.getMinutes().toString().length === 1 ? '0' + date.getMinutes() : date.getMinutes()}`;
    return `${day}.${month}.${year} ${hourse}:${minute}`;
  };

  const state = reactive({
    content: '<p>2333</p>',
    _content: '',
    editorOption: {
      placeholder: 'core',
      modules: {
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
      },
      // more options
    },
    disabled: false
  })
  const onEditorBlur = (quill) => {
    console.log("editor blur!", quill);
  }
  const onEditorFocus = (quill) => {
    console.log("editor focus!", quill);
  }
  const onEditorReady = (quill) => {
    console.log("editor ready!", quill);
  }
  const onEditorChange = ({ quill, html, text }) => {
    console.log('editor change!', quill, html, text);
    state._content = html;
  }

  const triggerUploadFile = () => {
    const fileInput = document.querySelector('input[type="file"]');
    if (fileInput) {
      fileInput.click();
    }
  };

  const imgObj = reactive({
    imgName: '',
    imgSrc: '',
    imgHeight: 0,
    imgWidth: 0
  });

  const uploadFile = (event) => {
    const file = event.target.files[0];
    if (file) {
      const img = new Image();
      const objectUrl = URL.createObjectURL(file);

      img.onload = () => {
        imgObj.imgName = file.name;
        imgObj.imgSrc = objectUrl;
        imgObj.imgWidth = img.width;
        imgObj.imgHeight = img.height;
      }
      img.src = objectUrl;
    }
  }

</script>

<style>
  .custom-datepicker .dp__input {
    height: 2.25rem; /* h-9 соответствует 2.25rem */
    line-height: 2.25rem; /* Убедитесь, что текст правильно выровнен по вертикали */
  }
</style>
<style>

  .vue-advanced-cropper__background {
    background-image: url("../../assets/black-white.png");
  }
</style>
