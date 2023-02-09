import axios, { AxiosResponse } from 'axios';
import { JobPosition } from '../models/JobPosition';
import { User, UserFormValues } from '../models/User';

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = 'http://localhost:5048/api/v1';

axios.interceptors.response.use(async (response) => {
  try {
    await sleep(400);
    return response;
  } catch (error) {
    console.log(error);
    return Promise.reject(error);
  }
});

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const JobPositions = {
  list: () => requests.get<JobPosition[]>('/jobposition'),
};

const Account = {
  current: () => requests.get<User>('/account'),
  login: (user: UserFormValues) => requests.post<UserFormValues>('/account/login', user),
  register: (user: UserFormValues) => requests.post<User>('/account/register', user),
};

const agent = {
  JobPositions,
  Account,
};

export default agent;