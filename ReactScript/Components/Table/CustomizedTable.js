import React from 'react'
import { Table, Loader } from 'semantic-ui-react'
import _ from 'lodash'
import PropTypes from 'prop-types'
import THead from './THead'
import TBody from './TBody'
import CustomizedPagination from './CustomizedPagination'

const CustomizedTable = ({
  isLoading,
  headerData,
  bodyData,
  handleButtonClick
}) => {
  return (
    <>
      <Table celled striped>
        <THead data={headerData} />
        {!isLoading && (
          <TBody
            col={headerData}
            data={bodyData}
            handleButtonClick={handleButtonClick}
          />
        )}
      </Table>
      {isLoading && <Loader as="span" active inline="centered" />}
      <CustomizedPagination />
    </>
  )
}

CustomizedTable.propTypes = {
  bodyData: PropTypes.array.isRequired,
  headerData: PropTypes.array.isRequired,
  handleButtonClick: PropTypes.func.isRequired,
  isLoading: PropTypes.bool.isRequired
}

export default CustomizedTable
