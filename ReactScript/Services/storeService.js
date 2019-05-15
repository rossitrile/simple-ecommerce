import http from './httpService'

const endPoint = http.apiEndpoint

const getStoresEndPoint = () => endPoint + '/api/store'
const getStoresByQueryEndPoint = query => endPoint + '/api/store?' + query
const getStoreEndPoint = id => endPoint + '/api/store/' + id
const createStoreEndPoint = () => endPoint + '/api/store'
const updateStoreEndPoint = id => endPoint + '/api/store/' + id
const deleteStoreEndPoint = id => endPoint + '/api/store/' + id

export {
  getStoresEndPoint,
  getStoreEndPoint,
  createStoreEndPoint,
  updateStoreEndPoint,
  deleteStoreEndPoint,
  getStoresByQueryEndPoint
}
