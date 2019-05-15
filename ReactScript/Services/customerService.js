import http from './httpService'

const endPoint = http.apiEndpoint

const getCustomersEndPoint = () => endPoint + '/api/customer'
const getCustomersByQueryEndPoint = query => endPoint + '/api/customer?' + query
const getCustomerEndPoint = id => endPoint + '/api/customer/' + id
const createCustomerEndPoint = () => endPoint + '/api/customer'
const updateCustomerEndPoint = id => endPoint + '/api/customer/' + id || ''
const deleteCustomerEndPoint = id => endPoint + '/api/customer/' + id || ''

export {
  getCustomersEndPoint,
  getCustomerEndPoint,
  createCustomerEndPoint,
  updateCustomerEndPoint,
  deleteCustomerEndPoint,
  getCustomersByQueryEndPoint
}
