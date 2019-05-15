import React from 'react'
import { Menu, Segment } from 'semantic-ui-react'
import { withRouter } from 'react-router-dom'

const NavBar = ({ history }) => {
  const currentLocation = history.location.pathname

  const navigatePage = page => history.push(page)

  return (
    <Segment inverted>
      <Menu inverted pointing secondary>
        <Menu.Item header onClick={() => navigatePage('/Customers')}>
          Rossi
        </Menu.Item>
        <Menu.Item
          name="Customers"
          active={currentLocation === '/Customers'}
          onClick={() => navigatePage('/Customers')}
        />
        <Menu.Item
          name="Products"
          active={currentLocation === '/Products'}
          onClick={() => navigatePage('/Products')}
        />
        <Menu.Item
          name="Stores"
          active={currentLocation === '/Stores'}
          onClick={() => navigatePage('/Stores')}
        />
        <Menu.Item
          name="Sales"
          active={currentLocation === '/Sales'}
          onClick={() => navigatePage('/Sales')}
        />
      </Menu>
    </Segment>
  )
}

export default withRouter(NavBar)
