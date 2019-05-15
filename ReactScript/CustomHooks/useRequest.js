import { useState } from 'react'

import http from '../Services/httpService'

const useRequest = initialData => {
  const [url, setUrl] = useState(() => '')
  const [request, setRequest] = useState(() => ({
    data: initialData || null,
    errors: [],
    loading: false
  }))

  const fetchInitial = async () => {
    if (!url) return
    setRequest({ ...request, loading: true })
    try {
      const { data } = await http.get(url)
      setRequest({
        data,
        errors: [],
        loading: false
      })
    } catch (e) {
      setRequest({ ...request, loading: false, errors: e.response.data })
    }
  }

  const fetch = async url => {
    setRequest({ ...request, loading: true })
    try {
      const { data } = await http.get(url)
      setRequest({
        errors: [],
        loading: false
      })
      return data
    } catch (e) {
      setRequest({ ...request, loading: false, errors: e.response.data })
    }
  }
  const post = async (url, data) => {
    setRequest({ ...request, loading: true })
    try {
      const response = await http.post(url, data)
      setRequest({
        errors: [],
        loading: false
      })
      return response.data
    } catch (e) {
      setRequest({ ...request, loading: false, errors: e.response.data })
    }
  }

  const remove = async url => {
    setRequest({ ...request, loading: true })
    try {
      const response = await http.delete(url)
      setRequest({
        errors: [],
        loading: false
      })
      return response.data
    } catch (e) {
      setRequest({ ...request, loading: false, errors: e.response.data })
    }
  }

  const update = async (url, data) => {
    setRequest({ ...request, loading: true })
    try {
      const response = await http.put(url, data)
      setRequest({
        errors: [],
        loading: false
      })
      return response.data
    } catch (e) {
      setRequest({ ...request, loading: false, errors: e.response.data })
    }
  }

  return { request, fetch, post, remove, update }
}

export default useRequest
