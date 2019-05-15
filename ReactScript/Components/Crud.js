import React, { useContext } from 'react'
import PropTypes from 'prop-types'

import { Context } from '../store'
import TriggerButton from './Table/TriggerButton'
import CustomizedTable from './Table/CustomizedTable'
import PopulateDataButton from './Table/PopulateDataButton'

const Crud = ({
  headerData,
  bodyData,
  isLoading,
  errors,
  textButton,
  tableName,
  actionVerb,
  createEndpoint,
  deleteEndpoint,
  updateEndpoint
}) => {
  const { actions } = useContext(Context)

  const openModal = (mode, fields) => {
    if (mode === 'create')
      actions({
        type: 'TRIGGER_MODAL',
        payload: {
          title: `Create ${tableName}`,
          fields: headerData,
          button: { label: 'checkmark', content: 'Create' },
          requestUrl: createEndpoint,
          actionName: actionVerb,
          actionData: tableName
        }
      })
    else if (mode === 'edit')
      actions({
        type: 'TRIGGER_MODAL',
        payload: {
          title: `Edit ${tableName}`,
          fields,
          button: { label: 'edit outline', content: 'Edit' },
          // /update/{id}
          requestUrl: updateEndpoint + fields[0].defaultValue,
          actionName: actionVerb,
          actionData: tableName
        }
      })
    else
      actions({
        type: 'TRIGGER_MODAL',
        payload: {
          title: 'Are you sure you want to delete ?',
          fields,
          button: { label: 'trash alternate outline', content: 'Delete' },
          requestUrl: deleteEndpoint + fields[0].defaultValue,
          actionName: actionVerb,
          actionData: tableName
        }
      })
  }

  /**
   *   Error when fetching data
   */
  if (errors.length !== 0) {
    return errors.map(e => (
      <Message style={{ width: '100%' }} color="red">
        {e}
      </Message>
    ))
  }

  return (
    <div style={{ padding: '0 20px' }}>
      <div style={{ display: 'flex' }}>
        <TriggerButton
          headerData={headerData}
          textButton={textButton}
          openModal={openModal}
        />
        <div style={{ flexGrow: 1 }} />
        <PopulateDataButton />
      </div>
      <CustomizedTable
        headerData={headerData}
        bodyData={bodyData}
        handleButtonClick={openModal}
        isLoading={isLoading}
      />
    </div>
  )
}

Crud.defaultProps = {
  errors: []
}
Crud.propTypes = {
  headerData: PropTypes.array.isRequired,
  bodyData: PropTypes.array.isRequired,
  isLoading: PropTypes.bool.isRequired,
  errors: PropTypes.array,
  textButton: PropTypes.string.isRequired,
  tableName: PropTypes.string.isRequired,
  actionVerb: PropTypes.string.isRequired,
  createEndpoint: PropTypes.string.isRequired,
  deleteEndpoint: PropTypes.string.isRequired,
  updateEndpoint: PropTypes.string.isRequired
}

export default Crud
