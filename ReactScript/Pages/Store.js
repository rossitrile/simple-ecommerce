import React, { useContext, useEffect } from 'react'

import { Context } from '../store'
import {
  createStoreEndPoint,
  getStoresByQueryEndPoint,
  deleteStoreEndPoint,
  updateStoreEndPoint
} from '../Services/storeService'

import useRequest from '../CustomHooks/useRequest'
import Crud from '../Components/Crud'

const Store = () => {
  const { state, actions } = useContext(Context)
  const { fetch, request } = useRequest([])

  const headerData = [
    { name: 'StoreId', required: false },
    { name: 'Name', required: true },
    { name: 'Address', required: true }
  ]

  const fetchInitialStoreData = async () => {
    const { pageIndex, pageSize, sortOrder } = state.display
    const query = [
      `pageSize=${pageSize}`,
      `pageIndex=${pageIndex}`,
      `sortOrder=${sortOrder}`
    ].join('&')
    const resp = await fetch(getStoresByQueryEndPoint(query))
    if (resp) {
      actions({
        type: 'SET_STATE',
        payload: {
          ...state,
          store: resp.data,
          dataCount: resp.count
        }
      })
    }
  }

  useEffect(() => {
    fetchInitialStoreData()
  }, [state.display])

  // Todo : handle error
  return (
    <Crud
      textButton="Create Store"
      headerData={headerData}
      bodyData={state.store}
      isLoading={request.loading}
      errors={request.errors}
      tableName="store"
      actionVerb="SET_STORE"
      createEndpoint={createStoreEndPoint('')}
      deleteEndpoint={deleteStoreEndPoint('')}
      updateEndpoint={updateStoreEndPoint('')}
    />
  )
}

export default Store
