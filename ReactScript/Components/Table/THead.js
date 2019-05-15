import React, { useContext } from 'react'
import PropTypes from 'prop-types'
import { Table, Icon } from 'semantic-ui-react'
import { Context } from '../../store'

const THead = ({ data }) => {
  const {
    actions,
    state: { display }
  } = useContext(Context)

  const renderHeader = header => {
    const shouldDisplayIcon =
      display.sortOrder.includes(header.name) ||
      (!display.sortOrder && header.name.includes('Id'))
    const icon = display.sortOrder.includes('desc') ? 'arrow down' : 'arrow up'
    return (
      <>
        {header.name} {shouldDisplayIcon && <Icon name={icon} />}
      </>
    )
  }

  const handleSorting = h => {
    const sortBy = display.sortOrder.includes('desc')
      ? h.name
      : `${h.name}_desc`

    actions({ type: 'SET_SORT', payload: sortBy })
  }

  return (
    <Table.Header>
      <Table.Row>
        {data.map(header => (
          <Table.HeaderCell
            onClick={() => handleSorting(header)}
            key={header.name}
          >
            {renderHeader(header)}
          </Table.HeaderCell>
        ))}
        <Table.HeaderCell>Actions</Table.HeaderCell>
      </Table.Row>
    </Table.Header>
  )
}

THead.propTypes = {
  data: PropTypes.array.isRequired
}

export default THead
