import axios from 'axios'

axios.interceptors.response.use(null, error => {
  const expectedError =
    error.response &&
    error.response.status >= 400 &&
    error.response.status < 500

  if (!expectedError) {
    return Promise.reject({
      response: {
        data: ['Something unexpected has happened, please try again later']
      }
    })
  }

  return Promise.reject(error)
})

export default {
  apiEndpoint: '',
  get: axios.get,
  post: axios.post,
  put: axios.put,
  delete: axios.delete
}
