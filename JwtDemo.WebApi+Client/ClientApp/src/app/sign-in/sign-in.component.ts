import { Component, OnInit } from "@angular/core";
import { SignInModel } from "../Models/sign-in.model";

@Component({
  selector: "app-sign-in",
  templateUrl: "./sign-in.component.html",
  styleUrls: ["./sign-in.component.css"],
})
export class SignInComponent implements OnInit {
  model = new SignInModel();
  constructor() {}

  ngOnInit() {}
}
