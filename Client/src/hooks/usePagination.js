import { useState } from "react";
const usePagination = (initialPage, initialPageSize) => {
  const [pagination, setPagination] = useState({
    page: initialPage,
    pageSize: initialPageSize,
  });

  const setPaginationData = (pagination) => {
    setPagination(pagination);
  };

  const setPage = (page) => {
    setPagination({
      ...pagination,
      page: page,
    });
  };

  const setPageSize = (pageSize) => {
    const totalPages = Math.ceil(pagination.totalCount / pageSize);
    const page = totalPages < pagination.page ? totalPages : pagination.page;

    setPagination({
      ...pagination,
      page: page,
      pageSize: pageSize,
    });
  };

  return {
    pagination,
    setPage,
    setPageSize,
    setPaginationData,
  };
};

export default usePagination;
