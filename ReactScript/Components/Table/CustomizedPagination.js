import React, { useContext, useState } from 'react'
import { Context } from '../../store'
import styled from 'styled-components'
import { Dropdown, Pagination } from 'semantic-ui-react'

const StyledCustomizedPagination = styled.div`
    display: flex;

    .itemPerPage {
        display: flex,
        flex-direction: column 
    }
    .paginationMenu {
        align-self: flex-end;
    }
    .spliter {
        flex-grow: 1
    }
`

const getDropdownOptions = () => {
  return [
    { key: 5, text: '5', value: 5 },
    { key: 10, text: '10', value: 10 },
    { key: 15, text: '20', value: 15 },
    { key: 20, text: '25', value: 20 }
  ]
}

const CustomizedPagination = () => {
  const { state, actions } = useContext(Context)
  const [pageSize, setPageSize] = useState(() => state.display.pageSize)
  const [activePage, setActivePage] = useState(() => state.display.activePage)

  const getTotalPage = () => {
    return Math.ceil(state.dataCount / state.display.pageSize)
  }

  const handleDropdownOptionsChange = (e, { value }) => {
    setPageSize(value)
    actions({ type: 'SET_PAGE', payload: { pageSize: value } })
  }
  const handlePageChange = (e, { activePage }) => {
    setActivePage(activePage)

    actions({ type: 'SET_PAGE', payload: { pageIndex: activePage } })
  }

  return (
    <StyledCustomizedPagination>
      <div className="itemPerPage">
        <p>Item per page:</p>
        <Dropdown
          placeholder="Items per page ( default 5 )"
          search
          selection
          closeOnChange
          value={pageSize}
          onChange={handleDropdownOptionsChange}
          options={getDropdownOptions()}
        />
      </div>
      <div className="spliter" />
      <div>
        <p>{state.dataCount} items</p>
        <Pagination
          boundaryRange={0}
          defaultActivePage={1}
          ellipsisItem={null}
          firstItem={null}
          lastItem={null}
          siblingRange={1}
          onPageChange={handlePageChange}
          totalPages={getTotalPage()}
        />
      </div>
    </StyledCustomizedPagination>
  )
}

export default CustomizedPagination
