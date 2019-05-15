import React from 'react'
import PropTypes from 'prop-types'
import moment from 'moment'
import { Table, Button } from 'semantic-ui-react'

const TBody = ({ col, data, handleButtonClick }) => {
  // convert from headerData to an array of fields with current value for modal
  const convertToArray = input => {
    return Object.keys(input).reduce((result, next, i) => {
      result.push({
        name: next,
        required: col[i].required,
        defaultValue: input[next],
        type: col[i].type,
        disabled: col[i].disable,
        url: col[i].url
      })
      return result
    }, [])
  }
  let keys = []
  if (data.length !== 0) keys = Object.keys(data[0])

  return (
    <Table.Body>
      {data.map(c => {
        return (
          <Table.Row key={c[keys[0]]}>
            {keys.map(k => {
              if (k.toUpperCase().includes('DATE'))
                return (
                  <Table.Cell key={k}>{moment(c[k]).format('llll')}</Table.Cell>
                )
              return <Table.Cell key={k}>{c[k]}</Table.Cell>
            })}
            <Table.Cell width="3">
              <Button
                positive
                icon="edit outline"
                labelPosition="right"
                content="edit"
                primary
                onClick={() => handleButtonClick('edit', convertToArray(c))}
              />
              <Button
                positive
                icon="trash alternate outline"
                labelPosition="right"
                content="delete"
                negative
                onClick={() => handleButtonClick('delete', convertToArray(c))}
              />
            </Table.Cell>
          </Table.Row>
        )
      })}
    </Table.Body>
  )
}

TBody.propTypes = {
  data: PropTypes.array.isRequired,
  col: PropTypes.array.isRequired,
  handleButtonClick: PropTypes.func.isRequired
}
export default TBody
