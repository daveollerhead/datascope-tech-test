import React from "react";

const TableHeader = (props) => {
  const { columns } = props;
  return (
    <thead>
      <tr>
        {columns.map((x) => (
          <th key={x.path}>{x.label}</th>
        ))}
      </tr>
    </thead>
  );
};

export default TableHeader;
