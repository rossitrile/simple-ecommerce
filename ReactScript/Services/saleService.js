import http from './httpService'

const endPoint = http.apiEndpoint

const getSalesEndPoint = () => endPoint + '/api/sale'
const getSalesByQueryEndPoint = query => endPoint + '/api/sale?' + query
const getSaleEndPoint = id => endPoint + '/api/sale/' + id
const createSaleEndPoint = () => endPoint + '/api/sale'
const updateSaleEndPoint = id => endPoint + '/api/sale/' + id
const deleteSaleEndPoint = id => endPoint + '/api/sale/' + id

export {
  getSalesEndPoint,
  getSaleEndPoint,
  createSaleEndPoint,
  updateSaleEndPoint,
  deleteSaleEndPoint,
  getSalesByQueryEndPoint
}
