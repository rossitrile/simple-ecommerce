import React, { useState } from 'react'
import { Button } from 'semantic-ui-react'
import http from '../../Services/httpService'

const PopulateDataButton = () => {
  const [isLoading, setIsLoading] = useState(() => false)

  const populateData = async () => {
    setIsLoading(true)
    try {
      await http.get('/api/populate')
      setIsLoading(false)
      window.location.href = '/'
    } catch (error) {
      setIsLoading(false)
      alert('Please try again later')
    }
  }

  return (
    <Button
      loading={isLoading}
      onClick={populateData}
      content="Populate Test Data"
      primary
    />
  )
}

export default PopulateDataButton
