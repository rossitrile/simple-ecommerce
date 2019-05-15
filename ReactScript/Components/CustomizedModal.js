import React, { useContext, useState } from 'react'
import { Modal, Button, Input, Message, Dropdown } from 'semantic-ui-react'
import moment from 'moment'

import { Context } from '../store'
import useRequest from '../CustomHooks/useRequest'
const capitalizeString = str => str[0].toUpperCase() + str.substr(1)

const CustomizedModal = () => {
  const { state, actions } = useContext(Context)
  const { post, remove, update, request, fetch } = useRequest()

  // utilize the fields of input to prepare initialState
  const getInitialInputs = () => {
    let initialInputs = state.modalData.fields.reduce((result, next) => {
      if (next.url) result[next.name.toLowerCase() + 'Id'] = ''
      else if (
        next.name.toUpperCase().includes('DATE') &&
        state.modalData.button.content === 'Create'
      )
        result[next.name] = moment().format('llll')
      else if (
        next.name.toUpperCase().includes('DATE') &&
        state.modalData.button.content === 'Edit'
      )
        result[capitalizeString(next.name)] = moment(next.defaultValue).format(
          'llll'
        )
      else result[capitalizeString(next.name)] = next.defaultValue || ''
      return result
    }, {})

    return initialInputs
  }

  const getDropDownOptions = async ({ url, name, defaultValue }) => {
    const data = await fetch(url)
    const id = name.toLowerCase() + 'Id'

    const options = data.reduce((result, next) => {
      result.push({
        key: next[id],
        text: next['name'],
        value: next[id]
      })
      return result
    }, [])

    const defaultOptionIndex = options.findIndex(op => op.text === defaultValue)
    const defaultOption = options[defaultOptionIndex]

    if (defaultValue && !inputs[id])
      setInputs({ ...inputs, [id]: defaultOption.key })
    setDropDownOptions({ ...dropDownOptions, [name]: options })
  }

  const [inputs, setInputs] = useState(() => getInitialInputs())
  const [dropDownOptions, setDropDownOptions] = useState(() => ({}))
  const [successMessage, setSuccessMessage] = useState(() => '')

  const handleDropDownValueChange = (event, { value, name }) =>
    setInputs({ ...inputs, [name]: value })

  const renderInputs = () =>
    state.modalData.fields.map((f, index) => {
      if (f.type === 'dropdown') {
        return (
          <Dropdown
            key={index}
            value={inputs[f.name.toLowerCase() + 'Id']}
            name={f.name.toLowerCase() + 'Id'}
            onChange={handleDropDownValueChange}
            style={{ width: '100%', marginBottom: '20px' }}
            clearable
            options={dropDownOptions[f.name]}
            selection
            onOpen={() => getDropDownOptions(f)}
            placeholder={f.defaultValue || f.name.toUpperCase()}
            disabled={state.modalData.button.content === 'Delete'}
          />
        )
      }
      return (
        <Input
          key={index}
          disabled={
            f.name.toUpperCase().includes('ID') ||
            f.name.toUpperCase().includes('DATE') ||
            state.modalData.button.content === 'Delete'
          }
          type={f.type || 'text'}
          required={f.required}
          label={f.required ? { icon: 'asterisk' } : false}
          labelPosition="right corner"
          placeholder={f.name.toUpperCase()}
          style={{ width: '100%', marginBottom: '20px' }}
          value={inputs[capitalizeString(f.name)]}
          name={capitalizeString(f.name)}
          onChange={handleInputChange}
        />
      )
    })

  const handleInputChange = ({ target: { value, name } }) =>
    setInputs({ ...inputs, [name]: value })

  const closeModal = () => actions({ type: 'CLOSE_MODAL' })

  const submitModal = async () => {
    const modalMode = state.modalData.button.content
    const { requestUrl, actionName, actionData } = state.modalData
    const requestData = { ...inputs }

    delete requestData[Object.keys(inputs)[0]]

    if (modalMode === 'Create') {
      const res = await post(requestUrl, requestData)

      if (res) {
        setSuccessMessage(modalMode + 'd')
        actions({ type: actionName, payload: [...state[actionData], res] })
      }
    } else if (modalMode === 'Delete') {
      // Remove data from the database
      const res = await remove(requestUrl)

      if (res) {
        setSuccessMessage(modalMode + 'd')
        // Remove data in the single source of truth
        const originalData = [...state[actionData]]
        const removedData = originalData.filter(
          d =>
            d[state.modalData.fields[0].name] !==
            state.modalData.fields[0].defaultValue
        )
        actions({ type: actionName, payload: removedData })
      }
    } else {
      // Update data from the database
      const res = await update(requestUrl, requestData)
      if (res) {
        setSuccessMessage(modalMode + 'd')

        // Update data in the single source of truth
        const originalData = [...state[actionData]]
        const updatedData = originalData.map(d => {
          if (
            d[state.modalData.fields[0].name] ===
            state.modalData.fields[0].defaultValue
          )
            state.modalData.fields.forEach(
              col => (d[col.name] = inputs[capitalizeString(col.name)])
            )
          return d
        })

        actions({ type: actionName, payload: updatedData })
        return
      }
    }

    setInputs(getInitialInputs())
  }

  return (
    <Modal
      size="tiny"
      open={state.isModalOpen}
      centered={false}
      style={{ top: '30%' }}
    >
      <Modal.Header>{state.modalData.title}</Modal.Header>
      <Modal.Content>
        {renderInputs()}
        {request.errors &&
          request.errors.map(e => (
            <Message key={e} color="red">
              {e}
            </Message>
          ))}
        {successMessage && <Message color="blue">{successMessage}</Message>}
      </Modal.Content>
      <Modal.Actions>
        <Button
          onClick={closeModal}
          negative
          icon="cancel"
          labelPosition="right"
          content="Cancel"
          disabled={request.loading}
        />
        <Button
          positive
          icon={state.modalData.button.label}
          labelPosition="right"
          content={state.modalData.button.content}
          onClick={submitModal}
          disabled={
            request.loading ||
            (state.modalData.button.content === 'Delete' &&
              Boolean(successMessage))
          }
        />
      </Modal.Actions>
    </Modal>
  )
}

export default CustomizedModal
