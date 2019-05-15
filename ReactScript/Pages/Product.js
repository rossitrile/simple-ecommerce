import React, { useContext, useEffect } from 'react'

import { Context } from '../store'
import {
  createProductEndPoint,
  getProductsByQueryEndPoint,
  deleteProductEndPoint,
  updateProductEndPoint
} from '../Services/productService'

import useRequest from '../CustomHooks/useRequest'
import Crud from '../Components/Crud'

const Product = () => {
  const { state, actions } = useContext(Context)
  const { fetch, request } = useRequest([])

  const headerData = [
    { name: 'ProductId', required: false },
    { name: 'Name', required: true },
    { name: 'Price', required: true, type: 'Number' }
  ]

  const fetchInitialProductData = async () => {
    const { pageIndex, pageSize, sortOrder } = state.display
    const query = [
      `pageSize=${pageSize}`,
      `pageIndex=${pageIndex}`,
      `sortOrder=${sortOrder}`
    ].join('&')

    const resp = await fetch(getProductsByQueryEndPoint(query))
    if (resp) {
      actions({
        type: 'SET_STATE',
        payload: {
          ...state,
          product: resp.data,
          dataCount: resp.count
        }
      })
    }
  }

  useEffect(() => {
    fetchInitialProductData()
  }, [state.display])

  return (
    <Crud
      textButton="Create Product"
      headerData={headerData}
      bodyData={state.product}
      isLoading={request.loading}
      errors={request.errors}
      tableName="product"
      actionVerb="SET_PRODUCT"
      createEndpoint={createProductEndPoint('')}
      deleteEndpoint={deleteProductEndPoint('')}
      updateEndpoint={updateProductEndPoint('')}
    />
  )
}

export default Product
