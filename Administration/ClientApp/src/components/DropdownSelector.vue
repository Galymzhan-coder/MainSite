<template>
  <div>
    <label for="dropdown">Выберите значение:</label>
    <select id="dropdown" :value="modelValue" @change="onSelectChange">
      <option v-for="item in items" :key="item[idField]" :value="item[idField]">
        {{ item[displayField] }}
      </option>
    </select>
  </div>
</template>

<script>
  export default {
    name: 'Dropdown',
    props: {
      items: {
        type: Array,
        required: true
      },
      idField: {
        type: String,
        required: true
      },
      displayField: {
        type: String,
        required: true
      },
      modelValue: {
        type: [Number, String],
        default: null
      }
    },
    methods: {
      onSelectChange(event) {
        this.$emit('update:modelValue', event.target.value);
      }
    }
  };
</script>

<style scoped>
</style>
