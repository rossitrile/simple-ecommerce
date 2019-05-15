import React, { useContext, useEffect } from 'react'
import { Message } from 'semantic-ui-react'

import {
  getCustomersByQueryEndPoint,
  createCustomerEndPoint,
  deleteCustomerEndPoint,
  updateCustomerEndPoint
} from '../Services/customerService'
import { Context } from '../store'
import useRequest from '../CustomHooks/useRequest'

import Crud from '../Components/Crud'

const Customer = () => {
  const { state, actions } = useContext(Context)
  const { fetch, request } = useRequest([])

  // Define the column of the table
  const headerData = [
    { name: 'CustomerId', required: false },
    { name: 'Name', required: true },
    { name: 'Address', required: true }
  ]

  const fetchInitialCustomerData = async () => {
    const { pageIndex, pageSize, sortOrder } = state.display
    const query = [
      `pageSize=${pageSize}`,
      `pageIndex=${pageIndex}`,
      `sortOrder=${sortOrder}`
    ].join('&')

    const resp = await fetch(getCustomersByQueryEndPoint(query))

    if (resp) {
      actions({
        type: 'SET_STATE',
        payload: {
          ...state,
          customer: resp.data,
          dataCount: resp.count
        }
      })
    }
  }

  // Update when user want to change setting of Pagination / Sorting
  useEffect(() => {
    fetchInitialCustomerData()
  }, [state.display])

  return (
    <Crud
      textButton="Create Customer"
      headerData={headerData}
      bodyData={state.customer}
      isLoading={request.loading}
      errors={request.errors}
      tableName="customer"
      actionVerb="SET_CUSTOMER"
      createEndpoint={createCustomerEndPoint('')}
      deleteEndpoint={deleteCustomerEndPoint('')}
      updateEndpoint={updateCustomerEndPoint('')}
    />
  )
}

export default Customer
