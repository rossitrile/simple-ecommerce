import React, { useContext, useEffect } from 'react'

import { Context } from '../store'
import {
  createSaleEndPoint,
  getSalesByQueryEndPoint,
  deleteSaleEndPoint,
  updateSaleEndPoint
} from '../Services/saleService'
import { getCustomersEndPoint } from '../Services/customerService'
import { getStoresEndPoint } from '../Services/storeService'
import { getProductsEndPoint } from '../Services/productService'

import useRequest from '../CustomHooks/useRequest'
import Crud from '../Components/Crud'

const Sale = () => {
  const { state, actions } = useContext(Context)
  const { fetch, request } = useRequest([])

  const headerData = [
    { name: 'SaleId', required: false },
    { name: 'DateSold', required: true, disable: true },
    {
      name: 'Customer',
      required: true,
      type: 'dropdown',
      url: getCustomersEndPoint()
    },
    {
      name: 'Store',
      required: true,
      type: 'dropdown',
      url: getStoresEndPoint()
    },
    {
      name: 'Product',
      required: true,
      type: 'dropdown',
      url: getProductsEndPoint()
    }
  ]

  const fetchInitialSaleData = async () => {
    const { pageIndex, pageSize, sortOrder } = state.display
    const query = [
      `pageSize=${pageSize}`,
      `pageIndex=${pageIndex}`,
      `sortOrder=${sortOrder}`
    ].join('&')

    const resp = await fetch(getSalesByQueryEndPoint(query))
    if (resp) {
      actions({
        type: 'SET_STATE',
        payload: {
          ...state,
          sale: resp.data,
          dataCount: resp.count
        }
      })
    }
  }

  useEffect(() => {
    fetchInitialSaleData()
  }, [state.display])

  return (
    <Crud
      textButton="Create Sale"
      headerData={headerData}
      bodyData={state.sale}
      isLoading={request.loading}
      errors={request.errors}
      tableName="sale"
      actionVerb="SET_SALE"
      createEndpoint={createSaleEndPoint('')}
      deleteEndpoint={deleteSaleEndPoint('')}
      updateEndpoint={updateSaleEndPoint('')}
    />
  )
}

export default Sale
