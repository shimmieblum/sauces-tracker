import {useState} from "react"
import axios from 'axios';
import {SauceResponse, SauceSchema} from "../models/SauceResponseSchema";
import {TraceEntryPointsPlugin} from "next/dist/build/webpack/plugins/next-trace-entrypoints-plugin";
import {SauceRequest} from "../models/SauceRequestSchema";
import {Task} from "@mui/icons-material";
import {end} from "@popperjs/core";

const baseUrl = 'http://localhost:5268/sauces'
export const useGetSauces = () => {
  const [ isLoading, setIsLoading] = useState(true);
  const GetSauces = async () => { 
    try{
      setIsLoading(true);
      const response = await axios.get<SauceResponse[]>(baseUrl);
      response.data.forEach(sauce => SauceSchema.parse(sauce));
      setIsLoading(false);
      return response.data;
    }
    catch(err){
      console.error(err, {message:'error occured'})
    }
  };
  return {isLoading, GetSauces};
}

export const useDeleteSauce = () => {
  const [ isDeleting, setIsDeleting ] = useState(false);
  const deleteSauce = async (id:string) => {
    try {
      setIsDeleting(true);
      const endpoint = `${baseUrl}/${id}`;
      const response = await axios.delete(endpoint);
      setIsDeleting(false);
      return response.status === axios.HttpStatusCode.Ok;
    }
    catch (err){
      console.error(err, {message: `error occured trying to delete sauce id=${id}`})
      return false;
    }
  }
  return { isDeleting, deleteSauce }
}

export const useCreateSauce = () => {
  const createSauce = async (request:SauceRequest) => {
    try {
      const endpoint = `${baseUrl}/with-fermentation`;
      const response = await axios.post(endpoint, request);
      console.log(response);
      return response.status === axios.HttpStatusCode.Ok;
    }
    catch (err){
      console.error(err, {message: `error creating sauce ${request.name}`})
      return false;
    }
  }
  
  return {createSauce};
}