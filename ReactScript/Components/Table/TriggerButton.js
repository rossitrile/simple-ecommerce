import React from 'react'
import { Button } from 'semantic-ui-react'

import PropTypes from 'prop-types'

const TriggerButton = ({ headerData, openModal, textButton }) => {
  // convert from headerData to an array of fields for modal
  const convertToArray = () => {
    return Object.keys(headerData).reduce((result, next, i) => {
      result.push({
        name: headerData[i].name,
        required: headerData[i].required,
        type: headerData[i].type,
        disabled: headerData[i].disable,
        url: headerData[i].url
      })
      return result
    }, [])
  }
  return (
    <Button
      onClick={() => openModal('create', convertToArray())}
      content={textButton}
      primary
    />
  )
}

TriggerButton.propTypes = {
  openModal: PropTypes.func.isRequired,
  textButton: PropTypes.string.isRequired
}

export default TriggerButton
