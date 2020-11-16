import { Component, Injectable, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NotifierService } from "angular-notifier";
import { NgxSpinnerService } from "ngx-spinner";
import { SignUpModel } from "../Models/sign-up.model";
import { AuthService } from "../Services/auth.service";

@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.css"],
})
export class SignUpComponent implements OnInit {
  model = new SignUpModel();
  confirmPassword: string;

  constructor(
    private spinner: NgxSpinnerService,
    private notifier: NotifierService,
    private auth: AuthService,
    private router: Router
  ) {}

  register() {
    this.spinner.show();
    this.notifier.hideAll();

    this.auth.SignUp(this.model).subscribe((data) => {
      console.log(data);
      if (data.statusCode === 200) {
        this.spinner.hide();
        this.router.navigate(["/sign-in"]);
      } else {
        console.log(data);
        for (var i = 0; i < data.errors.length; i++) {
          this.notifier.notify("error", data.errors[i]);
        }
      }
    });

    console.log(this.model);

    setTimeout(() => this.spinner.hide(), 2000);
  }

  ngOnInit() {}
}
