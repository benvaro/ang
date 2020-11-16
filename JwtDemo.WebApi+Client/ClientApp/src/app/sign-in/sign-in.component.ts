import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NotifierService } from "angular-notifier";
import { NgxSpinnerService } from "ngx-spinner";
import { SignInModel } from "../Models/sign-in.model";
import { AuthService } from "../Services/auth.service";
import jwt_decode from "jwt-decode";
import { decode } from "punycode";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.css"],
})
export class SignInComponent implements OnInit {
  model = new SignInModel();
  constructor(
    private auth: AuthService,
    private spinner: NgxSpinnerService,
    private notihier: NotifierService,
    private router: Router
  ) {}
  login() {
    this.spinner.show();
    this.auth.SignIn(this.model).subscribe((data) => {
      console.log(data);
      if (data.statusCode === 200) {
        this.spinner.hide();
        this.router.navigate([""]);
        window.localStorage.setItem("token", data.token);
        var decoded = jwt_decode(data.token);
        console.log(decoded);
        if (decoded.role == "Admin") this.router.navigate(["/admin-panel"]);
        else this.router.navigate(["/client-area"]);
      }
    });
  }
  ngOnInit() {}
}
