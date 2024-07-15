<template>
  <div class="pagination">
    <button @click="changePage(currentPage - 1)" :disabled="currentPage === 1">Previous</button>
    <button v-for="page in pages" :key="page" @click="changePage(page)" :class="{ active: page === currentPage }">
      {{ page }}
    </button>
    <button @click="changePage(currentPage + 1)" :disabled="currentPage === totalPages">Next</button>
  </div>
</template>

<script>
  export default {
    props: {
      currentPage: {
        type: Number,
        required: true
      },
      totalPages: {
        type: Number,
        required: true
      }
    },
    computed: {
      pages() {
        return Array.from({ length: this.totalPages }, (v, k) => k + 1);
      }
    },
    methods: {
      changePage(page) {
        console.log(`PageNavigationBar changePage(${page}), totalPages=`, this.totalPages);
        if (page > 0 && page <= this.totalPages) {
          this.$emit('page-changed', page);
        }
      }
    }
  };
</script>

<style scoped>
  .pagination {
    display: flex;
    justify-content: center;
    margin: 20px 0;
  }

    .pagination button {
      margin: 0 5px;
      padding: 5px 10px;
    }

      .pagination button.active {
        font-weight: bold;
        background-color: #007bff;
        color: white;
      }

      .pagination button:disabled {
        opacity: 0.5;
        cursor: not-allowed;
      }
</style>
