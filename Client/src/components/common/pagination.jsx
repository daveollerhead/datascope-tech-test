import React from "react";
import Pagination from "react-bootstrap/Pagination";
import _ from "lodash";

function PaginationComponent(props) {
  let pagination = props.pagination;
  if (pagination.totalPages === 1) {
    return null;
  }

  let active = pagination.page;
  let items = [];

  let paginationRange = 5;
  if (paginationRange > pagination.totalPages) {
    paginationRange = pagination.totalPages;
  }

  let start = pagination.page - Math.floor(paginationRange / 2);
  start = Math.max(start, 1);
  start = Math.min(start, 1 + pagination.totalPages - paginationRange);

  const pages = _.range(start, start + paginationRange);

  pages.forEach((page) => {
    items.push(
      <Pagination.Item
        key={page}
        active={page === active}
        onClick={() => props.onPageChange(page)}
      >
        {page}
      </Pagination.Item>
    );
  });

  return (
    <Pagination>
      <Pagination.First
        disabled={!pagination.hasPreviousPage}
        onClick={() => props.onPageChange(1)}
      />
      <Pagination.Prev
        disabled={!pagination.hasPreviousPage}
        onClick={() => props.onPageChange(pagination.page - 1)}
      />
      {props.pages > paginationRange && <Pagination.Ellipsis />}
      {items}
      {props.pages > paginationRange && <Pagination.Ellipsis />}
      <Pagination.Next
        disabled={!pagination.hasNextPage}
        onClick={() => props.onPageChange(pagination.page + 1)}
      />
      <Pagination.Last
        disabled={!pagination.hasNextPage}
        onClick={() => props.onPageChange(pagination.totalPages)}
      />
    </Pagination>
  );
}

export default PaginationComponent;
