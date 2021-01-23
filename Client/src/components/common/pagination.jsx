import React from "react";
import Pagination from "react-bootstrap/Pagination";
import _ from "lodash";

function PaginationComponent({ pagination, onPageChange }) {
  if (pagination.totalPages === 1) {
    return null;
  }

  const range = 5 > pagination.totalPages ? pagination.totalPages : 5;

  let start = pagination.page - Math.floor(range / 2);
  start = Math.max(start, 1);
  start = Math.min(start, 1 + pagination.totalPages - range);

  const pages = _.range(start, start + range);

  let items = [];
  pages.forEach((page) => {
    items.push(
      <Pagination.Item
        key={page}
        active={page === pagination.page}
        onClick={() => onPageChange(page)}
      >
        {page}
      </Pagination.Item>
    );
  });

  return (
    <Pagination>
      <Pagination.First
        disabled={!pagination.hasPreviousPage}
        onClick={() => onPageChange(1)}
      />
      <Pagination.Prev
        disabled={!pagination.hasPreviousPage}
        onClick={() => onPageChange(pagination.page - 1)}
      />
      {pagination.totalPages > range && <Pagination.Ellipsis />}
      {items}
      {pagination.totalPages > range && <Pagination.Ellipsis />}
      <Pagination.Next
        disabled={!pagination.hasNextPage}
        onClick={() => onPageChange(pagination.page + 1)}
      />
      <Pagination.Last
        disabled={!pagination.hasNextPage}
        onClick={() => onPageChange(pagination.totalPages)}
      />
    </Pagination>
  );
}

export default PaginationComponent;
