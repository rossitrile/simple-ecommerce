import React from 'react'
import { Route, Switch, Redirect } from 'react-router-dom'
import { createGlobalStyle } from 'styled-components'

import Customer from '../Pages/Customer'
import Product from '../Pages/Product'
import Store from '../Pages/Store'
import Sale from '../Pages/Sale'
import NavBar from '../Components/NavBar'
import CustomizedModal from '../Components/CustomizedModal'

import { Context, useGlobalStore } from '../store'

const GlobalStyle = createGlobalStyle`
  body, html {
    padding: 0;
    margin: 0;
  }
  *, *:before, *:after {
    padding: 0;
    margin: 0;
  }
`

const App = () => {
  const { state, actions } = useGlobalStore()

  return (
    <Context.Provider value={{ state, actions }}>
      {state.isModalOpen && <CustomizedModal />}
      <GlobalStyle />
      <NavBar />
      <Switch>
        <Route path="/Customers" component={Customer} />
        <Route path="/Products" component={Product} />
        <Route path="/Stores" component={Store} />
        <Route path="/Sales" component={Sale} />
        <Redirect to="/Customers" />
      </Switch>
    </Context.Provider>
  )
}

export default App
