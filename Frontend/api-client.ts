//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export class MatchClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl ?? "";
    }

    /**
     * @param id (optional) 
     * @return OK
     */
    get(id: number | undefined): Promise<MatchDto> {
        let url_ = this.baseUrl + "/Match/Get?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: Response): Promise<MatchDto> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = MatchDto.fromJS(resultData200);
            return result200;
            });
        } else if (status === 404) {
            return response.text().then((_responseText) => {
            return throwException("Not Found", status, _responseText, _headers);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<MatchDto>(null as any);
    }

    /**
     * @param id (optional) 
     * @param winnerId (optional) 
     * @param news (optional) 
     * @param extraInfo1 (optional) 
     * @param extraInfo2 (optional) 
     * @return OK
     */
    update(id: number | undefined, winnerId: number | undefined, news: string | undefined, extraInfo1: string | undefined, extraInfo2: string | undefined): Promise<MatchDto> {
        let url_ = this.baseUrl + "/Match/Update?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "Id=" + encodeURIComponent("" + id) + "&";
        if (winnerId === null)
            throw new Error("The parameter 'winnerId' cannot be null.");
        else if (winnerId !== undefined)
            url_ += "WinnerId=" + encodeURIComponent("" + winnerId) + "&";
        if (news === null)
            throw new Error("The parameter 'news' cannot be null.");
        else if (news !== undefined)
            url_ += "News=" + encodeURIComponent("" + news) + "&";
        if (extraInfo1 === null)
            throw new Error("The parameter 'extraInfo1' cannot be null.");
        else if (extraInfo1 !== undefined)
            url_ += "ExtraInfo1=" + encodeURIComponent("" + extraInfo1) + "&";
        if (extraInfo2 === null)
            throw new Error("The parameter 'extraInfo2' cannot be null.");
        else if (extraInfo2 !== undefined)
            url_ += "ExtraInfo2=" + encodeURIComponent("" + extraInfo2) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processUpdate(_response);
        });
    }

    protected processUpdate(response: Response): Promise<MatchDto> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = MatchDto.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            return throwException("Bad Request", status, _responseText, _headers);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<MatchDto>(null as any);
    }

    /**
     * @return OK
     */
    getAll(): Promise<MatchDto[]> {
        let url_ = this.baseUrl + "/Match/GetAll";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetAll(_response);
        });
    }

    protected processGetAll(response: Response): Promise<MatchDto[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(MatchDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<MatchDto[]>(null as any);
    }

    /**
     * @param player1Id (optional) 
     * @param player2Id (optional) 
     * @return Created
     */
    create(player1Id: number | undefined, player2Id: number | undefined): Promise<number> {
        let url_ = this.baseUrl + "/Match/Create?";
        if (player1Id === null)
            throw new Error("The parameter 'player1Id' cannot be null.");
        else if (player1Id !== undefined)
            url_ += "player1Id=" + encodeURIComponent("" + player1Id) + "&";
        if (player2Id === null)
            throw new Error("The parameter 'player2Id' cannot be null.");
        else if (player2Id !== undefined)
            url_ += "player2Id=" + encodeURIComponent("" + player2Id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processCreate(_response);
        });
    }

    protected processCreate(response: Response): Promise<number> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 201) {
            return response.text().then((_responseText) => {
            let result201: any = null;
            let resultData201 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result201 = resultData201 !== undefined ? resultData201 : <any>null;
    
            return result201;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            return throwException("Bad Request", status, _responseText, _headers);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<number>(null as any);
    }

    /**
     * @param id (optional) 
     * @return OK
     */
    delete(id: number | undefined): Promise<void> {
        let url_ = this.baseUrl + "/Match/Delete?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "DELETE",
            headers: {
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processDelete(_response);
        });
    }

    protected processDelete(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }
}

export class UserClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : window as any;
        this.baseUrl = baseUrl ?? "";
    }

    /**
     * @param id (optional) 
     * @return OK
     */
    get(id: number | undefined): Promise<UserDto> {
        let url_ = this.baseUrl + "/User/Get?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: Response): Promise<UserDto> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = UserDto.fromJS(resultData200);
            return result200;
            });
        } else if (status === 404) {
            return response.text().then((_responseText) => {
            return throwException("Not Found", status, _responseText, _headers);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<UserDto>(null as any);
    }

    /**
     * @return OK
     */
    getAll(): Promise<UserDto[]> {
        let url_ = this.baseUrl + "/User/GetAll";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetAll(_response);
        });
    }

    protected processGetAll(response: Response): Promise<UserDto[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(UserDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<UserDto[]>(null as any);
    }

    /**
     * @param name (optional) 
     * @param initials (optional) 
     * @return Created
     */
    create(name: string | undefined, initials: string | undefined): Promise<void> {
        let url_ = this.baseUrl + "/User/Create?";
        if (name === null)
            throw new Error("The parameter 'name' cannot be null.");
        else if (name !== undefined)
            url_ += "name=" + encodeURIComponent("" + name) + "&";
        if (initials === null)
            throw new Error("The parameter 'initials' cannot be null.");
        else if (initials !== undefined)
            url_ += "initials=" + encodeURIComponent("" + initials) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            headers: {
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processCreate(_response);
        });
    }

    protected processCreate(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 201) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            return throwException("Bad Request", status, _responseText, _headers);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }

    /**
     * @param id (optional) 
     * @return OK
     */
    delete(id: number | undefined): Promise<User> {
        let url_ = this.baseUrl + "/User/Delete?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "DELETE",
            headers: {
                "Accept": "text/plain"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processDelete(_response);
        });
    }

    protected processDelete(response: Response): Promise<User> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = User.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<User>(null as any);
    }
}

export class MatchDto implements IMatchDto {
    id?: number;
    player1!: UserDto;
    player2!: UserDto;
    winner?: UserDto;
    date?: Date | undefined;
    news?: string | undefined;
    extraInfo1?: string | undefined;
    extraInfo2?: string | undefined;

    constructor(data?: IMatchDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.player1 = new UserDto();
            this.player2 = new UserDto();
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.player1 = _data["player1"] ? UserDto.fromJS(_data["player1"]) : new UserDto();
            this.player2 = _data["player2"] ? UserDto.fromJS(_data["player2"]) : new UserDto();
            this.winner = _data["winner"] ? UserDto.fromJS(_data["winner"]) : <any>undefined;
            this.date = _data["date"] ? new Date(_data["date"].toString()) : <any>undefined;
            this.news = _data["news"];
            this.extraInfo1 = _data["extraInfo1"];
            this.extraInfo2 = _data["extraInfo2"];
        }
    }

    static fromJS(data: any): MatchDto {
        data = typeof data === 'object' ? data : {};
        let result = new MatchDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["player1"] = this.player1 ? this.player1.toJSON() : <any>undefined;
        data["player2"] = this.player2 ? this.player2.toJSON() : <any>undefined;
        data["winner"] = this.winner ? this.winner.toJSON() : <any>undefined;
        data["date"] = this.date ? this.date.toISOString() : <any>undefined;
        data["news"] = this.news;
        data["extraInfo1"] = this.extraInfo1;
        data["extraInfo2"] = this.extraInfo2;
        return data;
    }
}

export interface IMatchDto {
    id?: number;
    player1: UserDto;
    player2: UserDto;
    winner?: UserDto;
    date?: Date | undefined;
    news?: string | undefined;
    extraInfo1?: string | undefined;
    extraInfo2?: string | undefined;
}

export class User implements IUser {
    id?: number;
    name?: string | undefined;
    initials?: string | undefined;

    constructor(data?: IUser) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.initials = _data["initials"];
        }
    }

    static fromJS(data: any): User {
        data = typeof data === 'object' ? data : {};
        let result = new User();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["initials"] = this.initials;
        return data;
    }
}

export interface IUser {
    id?: number;
    name?: string | undefined;
    initials?: string | undefined;
}

export class UserDto implements IUserDto {
    id?: number;
    name?: string | undefined;
    initials?: string | undefined;

    constructor(data?: IUserDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.initials = _data["initials"];
        }
    }

    static fromJS(data: any): UserDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["initials"] = this.initials;
        return data;
    }
}

export interface IUserDto {
    id?: number;
    name?: string | undefined;
    initials?: string | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}