import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiResponce } from "../Models/api-responce.model";
import { SignInModel } from "../Models/sign-in.model";
import { SignUpModel } from "../Models/sign-up.model";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  baseUrl = "api/Account";

  constructor(private http: HttpClient) {}
  SignUp(model: SignUpModel): Observable<ApiResponce> {
    return this.http.post<ApiResponce>(this.baseUrl + "/register", model);
  }

  SignIn(model: SignInModel): Observable<ApiResponce> {
    return this.http.post<ApiResponce>(this.baseUrl + "/login", model);
  }
}
