// Single source of truth

import { createContext, useState } from 'react'

const initialModalState = {
  title: '',
  fields: [],
  button: {},
  loading: false,
  requestUrl: '',
  actionName: '',
  actionData: ''
}

const useGlobalStore = () => {
  const [state, setState] = useState(() => ({
    customer: [],
    product: [],
    store: [],
    sale: [],
    display: {
      pageIndex: 1,
      pageSize: 5,
      sortOrder: ''
    },
    dataCount: 0,
    modalData: initialModalState,
    isModalOpen: false
  }))

  const actions = ({ payload, type }) => {
    switch (type) {
      case 'SET_STATE':
        return setState(payload)
      case 'SET_CUSTOMER':
        return setState({ ...state, customer: payload })
      case 'SET_PRODUCT':
        return setState({ ...state, product: payload })
      case 'SET_STORE':
        return setState({ ...state, store: payload })
      case 'SET_SALE':
        return setState({ ...state, sale: payload })
      case 'TRIGGER_MODAL':
        return setState({ ...state, isModalOpen: true, modalData: payload })
      case 'SET_SORT':
        return setState({
          ...state,
          display: { ...state.display, sortOrder: payload }
        })
      case 'SET_PAGE':
        return setState({
          ...state,
          display: { ...state.display, ...payload }
        })
      case 'CLOSE_MODAL':
        return setState({
          ...state,
          isModalOpen: false,
          modal: initialModalState
        })
    }
  }

  return { actions, state }
}

const Context = createContext()

export { Context, useGlobalStore }
