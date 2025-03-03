import { useEffect, useState } from "react";
import { Category } from "../api/Models/Category";
import { apiService } from "../api/apiService";

const useCategories = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<Error | undefined>();
  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const categories = await apiService.getCategories();
        console.log(categories);
        setCategories(categories);
      } catch (error) {
        setError(error as Error);
      } finally {
        setLoading(false);
      }
    };
    fetchCategories();
  }, []);

  return { categories, loading, error };
};
export default useCategories;
