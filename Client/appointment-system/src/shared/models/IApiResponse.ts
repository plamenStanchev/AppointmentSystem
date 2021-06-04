export interface IApiResponse<T = unknown> {
  data: T;
  errors: string[] | null;
  isSuccessful: any;
}
