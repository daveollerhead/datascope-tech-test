import { useState } from "react";

const useLoading = (initialValue) => {
  const [isLoading, setIsLoading] = useState(initialValue);

  const loading = () => {
    setIsLoading(true);
  };

  const finishedLoading = () => {
    setIsLoading(false);
  };

  return {
    isLoading,
    loading,
    finishedLoading,
  };
};

export default useLoading;
