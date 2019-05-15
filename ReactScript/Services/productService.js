import http from './httpService'

const endPoint = http.apiEndpoint

const getProductsEndPoint = () => endPoint + '/api/product'
const getProductsByQueryEndPoint = query => endPoint + '/api/product?' + query
const getProductEndPoint = id => endPoint + '/api/product/' + id
const createProductEndPoint = () => endPoint + '/api/product'
const updateProductEndPoint = id => endPoint + '/api/product/' + id
const deleteProductEndPoint = id => endPoint + '/api/product/' + id

export {
  getProductsEndPoint,
  getProductEndPoint,
  createProductEndPoint,
  updateProductEndPoint,
  deleteProductEndPoint,
  getProductsByQueryEndPoint
}
