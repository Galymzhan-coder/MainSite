import store from "@/store";
import { createRouter, createWebHistory } from "vue-router";

import category from '../views/category/Category';
import categoryEdit from '../views/category/CategoryEdit';

import  TextPages  from '@/views/TextPages/TextPages';
import  TextPagesEdit  from '@/views/TextPages/TextPagesEdit';

import TopManagement from '../views/TopManagement/TopManagement';
import TopManagementEdit from '../views/TopManagement/TopManagementEdit';

import articlesEdit from '../views/CategoryAndContent/ArticlesEdit';

import BlogCategories from "@/views/Blog/categories/Category";
import BlogCategoriesCreate from "@/views/Blog/categories/CategoryCreate";
import BlogCategoriesEdit from "@/views/Blog/categories/CategoryEdit";

import Blog from "@/views/Blog/default/Blog";
import BlogEdit from "@/views/Blog/default/BlogEdit";
import Cities from "@/views/Contacts/Cities/Index";


const routes = [
  {
    path: "/",
    name: "Home",
    redirect: "/auth",
  },
  {
    path: "/auth",
    name: "Auth",
    component: () => import("@/views/authorization/Login.vue"),
  },
  {
    path: "/dashboard",
    name: "Dashboard",
    meta: { requiresAuth: true },
    component: () => import("@/views/master/dashboard_new.vue"),
    children: [
      {
        path: "/articles",
        name: "Articles",
        component: () => import("@/views/CategoryAndContent/Articles.vue"),
        children: [
          {
            path: "/create",
            name: "CreateForm",
            component: () =>
              import("@/views/CategoryAndContent/CreateForm.vue"),
          },
        ],
      },
      {
        name: 'ArticlesEdit',
        path: '/ArticlesEdit/:id',
        component: articlesEdit,
      },
      {
        name: 'Category',
        path: '/Category',
        component: category,
      },
      {
        name: 'categoryEdit',
        path: '/categoryEdit/:id',
        component: categoryEdit,
      },
      {
        name: 'TextPages',
        path: '/TextPages',
        component: TextPages,
      },
      {
        name: 'TextPagesEdit',
        path: '/TextPagesEdit/:id',
        component: TextPagesEdit,
      },
      {
        name: 'blog',
        path: '/blog',
        children: [
          {
            name: 'blog-index',
            path: 'index',
            component: Blog
          },
          {
            name: 'blog-update',
            path: 'update/:id',
            component: BlogEdit,
            props: true
          },
          {
            name: 'blog-category',
            path: 'category',
            children: [
              {
                name: 'blog-category-index',
                path: 'index',
                component: BlogCategories
              },
              {
                name: 'blog-category-create',
                path: 'create',
                component: BlogCategoriesCreate
              },
              {
                name: 'blog-category-update',
                path: 'update/:id',
                component: BlogCategoriesEdit,
                props: true
              },
            ]
          }
        ]
      },
      {
        name: 'contacts',
        path: '/contacts',
        children: [
          {
            name: 'cities',
            path: 'cities',
            children: [
              {
                name: 'index',
                path: 'index',
                component: Cities
              }
            ]
          }
        ]
      },
      {
        name: 'TopManagement',
        path: '/TopManagement',
        component: TopManagement,
      },
      {
        name: 'TopManagementEdit',
        path: '/TopManagement/:id',
        component: TopManagementEdit,
      },
    ],
  },
];

let base = "admin.vue/";

const router = createRouter({
  history: createWebHistory(),
  base,
  routes,
  linkActiveClass: "active",
});

/* Проверка верификация */

router.beforeEach(async (to, from, next) => {
  if (to.meta.requiresAuth) {
    if (store.state.authStore.token) {
      next();
    } else {
      next({ name: "Auth" });
    }
  } else {
    next();
  }
});

export default router;
