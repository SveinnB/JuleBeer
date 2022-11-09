import { ToBase64, imageTypes } from '~/utils/utils';

// only set to active Vuex
export const state = () => ({
  users: null,
  error: null,
  tenant: null,
  logo: null,
  isColorInTesting: false,
  lang: 'is'
})

export const mutations = {
  SET_USERS(state, users){
    state.users = users;
  },
  SET_TENANT(state, tenant){
    state.tenant = tenant;
  },
  SET_LOGO(state, logo){
    state.logo = logo
  },
  SET_ERROR(state, error){
    state.error = error;
  },
  SET_IS_COLOR_IN_TESTING(state, val){
    state.isColorInTesting = val;
  },
  SET_LANG(state, lang){
    state.lang = lang
  }
}

export const actions = {
  async fetchUsers({ commit }, $axios) {
    try{
      // Fetch all users
      const users = await $axios.$get('/user/all?onlyActive=false');
      commit('SET_USERS', users);
    }
    catch (e) {
      commit('SET_ERROR', e);
    }
  },

  async fetchMyTenantInfo({ commit }, $axios) {
    try {
      const tenantInfo = await $axios.$get('/Settings/GetMyTenantInfo');
      commit('SET_TENANT', tenantInfo);
    }
    catch (e){
      commit('SET_ERROR', e);
    }
  },

  async fetchMyTenantLogo({ commit }, $axios) {
    try {
      const blob = await $axios.$get('/Settings/GetMyTenantLogo', {
        responseType: 'blob'
      });
      if(blob && blob.size > 0 && imageTypes.includes(blob.type)){
        const base64 = await ToBase64(blob)
        blob.lastModifiedDate = new Date();
        blob.name = 'Logo';

        const logo = {
          file: blob,
          base64: base64
        }
        commit('SET_LOGO', logo);
      }
      else{
        commit('SET_LOGO', null);
      }
    }
    catch (e) {
      commit('SET_ERROR', e);
    }
  },

  setError({ commit }, error){
    commit('SET_ERROR', error);
  },
  setIsColorInTesting({ commit }, val){
    commit('SET_IS_COLOR_IN_TESTING', val);
  },
  setLogo({ commit }, logo){
    commit('SET_LOGO', logo);
  },
  setLang({ commit }, lang){
    commit('SET_LANG', lang)
  }
}